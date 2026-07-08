using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using ApiMantenimiento.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MMantenimiento
{
    public class PlanMantenimientoService : IPlanMantenimientoService
    {
        private readonly IPlanMantenimientoRepository _repository;
        private readonly MantenimientoDbContext _context;

        public PlanMantenimientoService(IPlanMantenimientoRepository repository, MantenimientoDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // ── GET ALL ──────────────────────────────────────────────
        public async Task<ApiResponse<IEnumerable<PlanMantenimientoResponse>>> GetAllAsync()
        {
            try
            {
                var planes = await _repository.GetAllAsync();
                var response = planes.Select(MapToResponse).ToList();
                return ApiResponse<IEnumerable<PlanMantenimientoResponse>>.SuccessResult(response);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<PlanMantenimientoResponse>>.Fail($"Error al obtener planes: {ex.Message}");
            }
        }

        // ── GET BY ID ────────────────────────────────────────────
        public async Task<ApiResponse<PlanMantenimientoResponse>> GetByIdAsync(int id)
        {
            try
            {
                var plan = await _repository.GetByIdAsync(id);
                if (plan == null)
                    return ApiResponse<PlanMantenimientoResponse>.Fail($"Plan de mantenimiento con id {id} no encontrado.");

                return ApiResponse<PlanMantenimientoResponse>.SuccessResult(MapToResponse(plan));
            }
            catch (Exception ex)
            {
                return ApiResponse<PlanMantenimientoResponse>.Fail($"Error al obtener el plan: {ex.Message}");
            }
        }

        // ── CREATE ───────────────────────────────────────────────
        public async Task<ApiResponse<PlanMantenimientoResponse>> CreateAsync(PlanMantenimientoRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var plan = new PlanMantenimiento
                {
                    id_estrategia = request.id_estrategia,
                    fecha_creacion = request.fecha_creacion ?? DateTime.UtcNow,
                    estado = request.estado,
                    PlanMantenimientoActividades = new List<PlanMantenimientoActividad>(),
                    PlanMantenimientoPersonales = (request.Personales ?? new List<PlanMantenimientoPersonalRequest>())
                        .Select(p => new PlanMantenimientoPersonal { id_rol = p.id_rol, cantidad = p.cantidad })
                        .ToList()
                };

                var actividadesList = request.Actividades ?? new List<PlanMantenimientoActividadRequest>();
                foreach (var a in actividadesList)
                {
                    int actId = await ResolverIdActividad(a);

                    // Evitar duplicados (misma actividad + mismo nivel PM)
                    bool duplicate = plan.PlanMantenimientoActividades
                        .Any(x => x.id_actividad == actId && x.id_detalle_estrg == a.id_detalle_estrg);
                    if (duplicate) continue;

                    var actRow = new PlanMantenimientoActividad
                    {
                        id_actividad  = actId,
                        id_detalle_estrg = a.id_detalle_estrg
                    };

                    // Material opcional → va a la tabla 3FN
                    if (a.id_material.HasValue && a.id_material.Value > 0)
                    {
                        actRow.Materiales.Add(new PlanActividadMaterial
                        {
                            id_material = a.id_material.Value,
                            cantidad    = a.cantidad ?? 1
                        });
                    }

                    plan.PlanMantenimientoActividades.Add(actRow);
                }

                await _context.PlanesMantenimiento.AddAsync(plan);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var full = await _repository.GetByIdAsync(plan.id_plan_mant);
                return ApiResponse<PlanMantenimientoResponse>.SuccessResult(MapToResponse(full), "Plan de mantenimiento creado exitosamente.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ApiResponse<PlanMantenimientoResponse>.Fail($"Error al crear el plan: {ex.Message}");
            }
        }

        // ── UPDATE ───────────────────────────────────────────────
        public async Task<ApiResponse<PlanMantenimientoResponse>> UpdateAsync(int id, PlanMantenimientoUpdateRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var plan = await _context.PlanesMantenimiento
                    .AsSplitQuery()
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.Materiales)
                    .Include(p => p.PlanMantenimientoPersonales)
                    .FirstOrDefaultAsync(p => p.id_plan_mant == id);

                if (plan == null)
                    return ApiResponse<PlanMantenimientoResponse>.Fail($"Plan con id {id} no encontrado.");

                plan.id_estrategia = request.id_estrategia;
                plan.estado        = request.estado;

                // Reemplazar actividades (EF cascade elimina los Materiales hijos)
                plan.PlanMantenimientoActividades.Clear();

                var actividadesList = request.Actividades ?? new List<PlanMantenimientoActividadRequest>();
                foreach (var a in actividadesList)
                {
                    int actId = await ResolverIdActividad(a);

                    bool duplicate = plan.PlanMantenimientoActividades
                        .Any(x => x.id_actividad == actId && x.id_detalle_estrg == a.id_detalle_estrg);
                    if (duplicate) continue;

                    var actRow = new PlanMantenimientoActividad
                    {
                        id_plan_mant     = id,
                        id_actividad     = actId,
                        id_detalle_estrg = a.id_detalle_estrg
                    };

                    if (a.id_material.HasValue && a.id_material.Value > 0)
                    {
                        actRow.Materiales.Add(new PlanActividadMaterial
                        {
                            id_plan_mant     = id,
                            id_actividad     = actId,
                            id_detalle_estrg = a.id_detalle_estrg,
                            id_material      = a.id_material.Value,
                            cantidad         = a.cantidad ?? 1
                        });
                    }

                    plan.PlanMantenimientoActividades.Add(actRow);
                }

                // Reemplazar personal
                plan.PlanMantenimientoPersonales.Clear();
                var personalesList = request.Personales ?? new List<PlanMantenimientoPersonalRequest>();
                foreach (var p in personalesList)
                {
                    plan.PlanMantenimientoPersonales.Add(new PlanMantenimientoPersonal
                    {
                        id_rol       = p.id_rol,
                        cantidad     = p.cantidad,
                        id_plan_mant = id
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var full = await _repository.GetByIdAsync(id);
                return ApiResponse<PlanMantenimientoResponse>.SuccessResult(MapToResponse(full), "Plan actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ApiResponse<PlanMantenimientoResponse>.Fail($"Error al actualizar el plan: {ex.Message}");
            }
        }

        // ── DELETE ───────────────────────────────────────────────
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var plan = await _repository.GetByIdAsync(id);
                if (plan == null)
                    return ApiResponse<bool>.Fail($"Plan con id {id} no encontrado.");

                await _repository.DeleteAsync(id);
                return ApiResponse<bool>.SuccessResult(true, "Plan eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"Error al eliminar el plan: {ex.Message}");
            }
        }

        // ── ACTIVIDADES ──────────────────────────────────────────
        public async Task<ApiResponse<PlanMantenimientoActividadResponse>> AddActividadAsync(int id_plan_mant, PlanMantenimientoActividadRequest request)
        {
            try
            {
                int actId = await ResolverIdActividad(request);

                var actividad = new PlanMantenimientoActividad
                {
                    id_plan_mant     = id_plan_mant,
                    id_actividad     = actId,
                    id_detalle_estrg = request.id_detalle_estrg
                };

                if (request.id_material.HasValue && request.id_material.Value > 0)
                {
                    actividad.Materiales.Add(new PlanActividadMaterial
                    {
                        id_plan_mant     = id_plan_mant,
                        id_actividad     = actId,
                        id_detalle_estrg = request.id_detalle_estrg,
                        id_material      = request.id_material.Value,
                        cantidad         = request.cantidad ?? 1
                    });
                }

                await _repository.AddActividadAsync(actividad);

                var plan = await _repository.GetByIdAsync(id_plan_mant);
                var added = plan.PlanMantenimientoActividades
                    .FirstOrDefault(a => a.id_actividad == actId && a.id_detalle_estrg == request.id_detalle_estrg);

                return ApiResponse<PlanMantenimientoActividadResponse>.SuccessResult(MapActividadToResponse(added));
            }
            catch (Exception ex)
            {
                return ApiResponse<PlanMantenimientoActividadResponse>.Fail($"Error al agregar actividad: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> RemoveActividadAsync(int id_plan_mant, int id_actividad, int id_detalle_estrg)
        {
            try
            {
                await _repository.RemoveActividadAsync(id_plan_mant, id_actividad, id_detalle_estrg);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"Error al remover actividad: {ex.Message}");
            }
        }

        // ── PERSONAL ─────────────────────────────────────────────
        public async Task<ApiResponse<PlanMantenimientoPersonalResponse>> AddPersonalAsync(int id_plan_mant, PlanMantenimientoPersonalRequest request)
        {
            try
            {
                var personal = new PlanMantenimientoPersonal
                {
                    id_plan_mant = id_plan_mant,
                    id_rol       = request.id_rol,
                    cantidad     = request.cantidad
                };
                await _repository.AddPersonalAsync(personal);

                var plan = await _repository.GetByIdAsync(id_plan_mant);
                var added = plan.PlanMantenimientoPersonales
                    .FirstOrDefault(p => p.id_rol == request.id_rol);

                return ApiResponse<PlanMantenimientoPersonalResponse>.SuccessResult(new PlanMantenimientoPersonalResponse
                {
                    id_plan_personal = added?.id_plan_personal ?? 0,
                    id_rol           = request.id_rol,
                    nombre_rol       = added?.Rol?.nombre_rol,
                    cantidad         = request.cantidad
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<PlanMantenimientoPersonalResponse>.Fail($"Error al agregar personal: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> RemovePersonalAsync(int id_plan_personal)
        {
            try
            {
                await _repository.RemovePersonalAsync(id_plan_personal);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"Error al remover personal: {ex.Message}");
            }
        }

        // ── HELPERS PRIVADOS ─────────────────────────────────────

        /// <summary>
        /// Si id_actividad > 0 lo devuelve directo.
        /// Si es 0, busca por nombre+sistema o crea la actividad nueva.
        /// </summary>
        private async Task<int> ResolverIdActividad(PlanMantenimientoActividadRequest a)
        {
            if (a.id_actividad > 0)
                return a.id_actividad;

            string normNombre = (a.nombre_actividad ?? "").Trim().ToLower();
            string normSist   = (a.cod_sistema ?? "").Trim().ToLower();

            var existingAct = _context.ActividadesSistemas
                .FirstOrDefault(x => x.nombre_actividad != null && x.SistemaEquipo != null
                                     && x.nombre_actividad.Trim().ToLower() == normNombre
                                     && x.SistemaEquipo.cod_sist.Trim().ToLower() == normSist);

            if (existingAct != null)
                return existingAct.id_actividad;

            var system = _context.SistemasEquipos
                .FirstOrDefault(s => s.cod_sist.Trim().ToLower() == normSist);

            if (system == null)
                throw new Exception($"El sistema con código '{a.cod_sistema}' no existe en la base de datos.");

            var count  = _context.ActividadesSistemas.Count() + 1;
            var codAct = $"ACT-{count:D4}";

            var newAct = new ActividadSistema
            {
                cod_act         = codAct,
                id_sistema      = system.id_sistema,
                nombre_actividad = a.nombre_actividad ?? "Nueva Actividad",
                descripcion     = a.nombre_actividad ?? "Nueva Actividad",
                duracion        = 1,
                medida_duracion = "H",
                estado          = true
            };
            _context.ActividadesSistemas.Add(newAct);
            await _context.SaveChangesAsync();
            return newAct.id_actividad;
        }

        // ── MAPPERS ──────────────────────────────────────────────
        private static PlanMantenimientoResponse MapToResponse(PlanMantenimiento plan)
        {
            return new PlanMantenimientoResponse
            {
                id_plan_mant   = plan.id_plan_mant,
                id_estrategia  = plan.id_estrategia,
                fecha_creacion = DateTime.SpecifyKind(plan.fecha_creacion, DateTimeKind.Utc),
                estado         = plan.estado,
                Estrategia = plan.Estrategia == null ? null : new EstrategiaResponse
                {
                    id_estrategia      = plan.Estrategia.id_estrategia,
                    cod_estrategia     = plan.Estrategia.cod_estrategia,
                    titulo_estrategia  = plan.Estrategia.titulo_estrategia,
                    id_flota           = plan.Estrategia.id_flota,
                    cod_flota          = plan.Estrategia.Flota?.cod_flota,
                    nombre_flota       = plan.Estrategia.Flota?.nombre_flota,
                    id_equipo          = plan.Estrategia.id_equipo,
                    cod_equipo         = plan.Estrategia.Equipo?.cod_eqp,
                    estado             = plan.Estrategia.estado,
                    Detalles = plan.Estrategia.Detalles?.Select(d => new EstrategiaDetalleResponse
                    {
                        id_detalle_estrg = d.id_detalle_estrg,
                        id_estrategia    = d.id_estrategia,
                        umbral_mant      = d.umbral_mant,
                        tolerancia_inf   = d.tolerancia_inf,
                        tolerancia_sup   = d.tolerancia_sup,
                        porcentaje_tol   = d.porcentaje_tol,
                        nombre_medida    = d.nombre_medida,
                        uni_med          = d.uni_med,
                        tipo_pm          = d.tipo_pm
                    }).ToList() ?? new()
                },
                Actividades = plan.PlanMantenimientoActividades?
                    .Select(MapActividadToResponse).ToList() ?? new(),
                Personales = plan.PlanMantenimientoPersonales?.Select(p => new PlanMantenimientoPersonalResponse
                {
                    id_plan_personal = p.id_plan_personal,
                    id_rol           = p.id_rol,
                    nombre_rol       = p.Rol?.nombre_rol,
                    cantidad         = p.cantidad
                }).ToList() ?? new()
            };
        }

        private static PlanMantenimientoActividadResponse MapActividadToResponse(PlanMantenimientoActividad a)
        {
            // Aplana el primer material de la colección 3FN (máx. 1 por actividad)
            var mat = a.Materiales?.FirstOrDefault();
            return new PlanMantenimientoActividadResponse
            {
                id_plan_mant          = a.id_plan_mant,
                id_actividad          = a.id_actividad,
                nombre_actividad      = a.ActividadSistema?.nombre_actividad,
                descripcion_actividad = a.ActividadSistema?.descripcion,
                cod_sistema           = a.ActividadSistema?.SistemaEquipo?.cod_sist,
                id_detalle_estrg      = a.id_detalle_estrg,
                tipo_pm               = a.EstrategiaDetalle?.tipo_pm,
                id_material           = mat?.id_material,
                cod_materia           = mat?.Material?.cod_materia,
                descripcion_material  = mat?.Material?.descripcion,
                cantidad              = mat?.cantidad
            };
        }
    }
}
