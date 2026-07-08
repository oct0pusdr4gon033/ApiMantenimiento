using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Services.MMantenimiento
{
    public class OrdenTrabajoService : IOrdenTrabajoService
    {
        private readonly IOrdenTrabajoRepository _otRepo;
        private readonly MantenimientoDbContext _context;

        public OrdenTrabajoService(IOrdenTrabajoRepository otRepo, MantenimientoDbContext context)
        {
            _otRepo   = otRepo;
            _context  = context;
        }

        // ── GET ALL ──────────────────────────────────────────────
        public async Task<ApiResponse<IEnumerable<OrdenTrabajoResponse>>> GetAllAsync()
        {
            try
            {
                var ots = await _otRepo.GetAllAsync();
                return ApiResponse<IEnumerable<OrdenTrabajoResponse>>.SuccessResult(
                    ots.Select(MapToResponse));
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<OrdenTrabajoResponse>>.Fail($"Error al obtener OTs: {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<OrdenTrabajoResponse>>> GetByEquipoAsync(int idEquipo)
        {
            try
            {
                var ots = await _otRepo.GetByEquipoAsync(idEquipo);
                return ApiResponse<IEnumerable<OrdenTrabajoResponse>>.SuccessResult(
                    ots.Select(MapToResponse));
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<OrdenTrabajoResponse>>.Fail($"Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<OrdenTrabajoResponse>> GetByIdAsync(int idOt)
        {
            try
            {
                var ot = await _otRepo.GetByIdAsync(idOt);
                if (ot == null)
                    return ApiResponse<OrdenTrabajoResponse>.Fail($"OT con id {idOt} no encontrada.");

                return ApiResponse<OrdenTrabajoResponse>.SuccessResult(MapToResponse(ot));
            }
            catch (Exception ex)
            {
                return ApiResponse<OrdenTrabajoResponse>.Fail($"Error: {ex.Message}");
            }
        }

        // ── CREATE MANUAL ────────────────────────────────────────
        public async Task<ApiResponse<OrdenTrabajoResponse>> CreateManualAsync(OrdenTrabajoCreateRequest request)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Flota)
                        .ThenInclude(f => f.ModeloEquipo)
                            .ThenInclude(m => m.Marca)
                    .FirstOrDefaultAsync(e => e.id_equipo == request.id_equipo);

                if (equipo == null)
                    return ApiResponse<OrdenTrabajoResponse>.Fail("Equipo no encontrado.");

                var plan = await _context.PlanesMantenimiento
                    .AsSplitQuery()
                    .Include(p => p.Estrategia)
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.ActividadSistema)
                            .ThenInclude(s => s.SistemaEquipo)
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.EstrategiaDetalle)
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.Materiales)
                            .ThenInclude(m => m.Material)
                    .FirstOrDefaultAsync(p => p.id_plan_mant == request.id_plan_mant);

                if (plan == null)
                    return ApiResponse<OrdenTrabajoResponse>.Fail("Plan de mantenimiento no encontrado.");

                var ot = ConstruirOT(
                    equipo, plan, request.ids_detalle_estrg,
                    request.tipo_ot, "MANUAL",
                    request.tipo_ot == "CORRECTIVA" && request.horometro_falla.HasValue ? request.horometro_falla.Value : equipo.horometro_actual,
                    request.observaciones, request.creado_por);

                if (request.tipo_ot == "CORRECTIVA")
                {
                    ot.hora_intervencion = request.hora_intervencion;
                    ot.descripcion_falla = request.descripcion_falla;
                    ot.id_sistema = request.id_sistema;
                    ot.id_subsistema = request.id_subsistema;
                    ot.horometro_falla = request.horometro_falla;
                }

                // Asignar personal manual
                if (request.personal_dni != null && request.personal_dni.Any())
                {
                    foreach (var dni in request.personal_dni)
                    {
                        ot.Personal.Add(new OTPersonal { dni_empleado = dni });
                    }
                }

                // Si es correctiva, asignar materiales manuales y crear actividad por defecto
                if (request.tipo_ot == "CORRECTIVA")
                {
                    if (request.materiales != null && request.materiales.Any())
                    {
                        foreach (var mat in request.materiales)
                        {
                            ot.Materiales.Add(new OTMaterial
                            {
                                id_material_ref = mat.id_material_ref,
                                cod_materia = mat.cod_materia,
                                descripcion_material = mat.descripcion_material,
                                cantidad_requerida = mat.cantidad_requerida
                            });
                        }
                    }

                    ot.Actividades.Add(new OTActividad
                    {
                        nombre_actividad = string.IsNullOrEmpty(request.observaciones)
                            ? "Atención Correctiva General"
                            : $"Reparación: {request.observaciones}",
                        cod_sistema = string.Empty,
                        tipo_pm = string.Empty,
                        observacion_tecnica = string.Empty,
                        estado_ejecucion = "PENDIENTE"
                    });
                }

                await _otRepo.CreateAsync(ot);

                // Actualizar calendario
                await RecalcularCalendarioAsync(equipo.id_equipo);

                var full = await _otRepo.GetByIdAsync(ot.id_ot);
                return ApiResponse<OrdenTrabajoResponse>.SuccessResult(MapToResponse(full), "OT creada exitosamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<OrdenTrabajoResponse>.Fail($"Error al crear OT: {ex.Message}");
            }
        }

        // ── CAMBIAR ESTADO ───────────────────────────────────────
        public async Task<ApiResponse<OrdenTrabajoResponse>> CambiarEstadoAsync(int idOt, CambiarEstadoOTRequest request)
        {
            try
            {
                var ot = await _otRepo.GetByIdAsync(idOt);
                if (ot == null)
                    return ApiResponse<OrdenTrabajoResponse>.Fail($"OT {idOt} no encontrada.");

                ValidarTransicionEstado(ot.estado, request.nuevo_estado);

                ot.estado = request.nuevo_estado?.Trim().ToUpper();

                if (!string.IsNullOrEmpty(request.observaciones))
                    ot.observaciones = request.observaciones;

                if (request.nuevo_estado?.Trim().ToUpper() == "CERRADA")
                {
                    if (!request.horometro_cierre.HasValue)
                        return ApiResponse<OrdenTrabajoResponse>.Fail("Se requiere el horómetro de cierre para cerrar la OT.");

                    if (ot.tipo_ot == "CORRECTIVA")
                    {
                        var hora = request.hora_intervencion ?? ot.hora_intervencion;
                        if (!hora.HasValue)
                            return ApiResponse<OrdenTrabajoResponse>.Fail("Se requiere la hora de inicio de intervención para cerrar la OT Correctiva.");

                        ot.hora_intervencion = hora;
                    }

                    ot.horometro_corte = request.horometro_cierre.Value;
                    ot.fecha_atencion  = DateTime.UtcNow;

                    // Actualizar cantidades reales de materiales
                    foreach (var mat in request.materiales_utilizados)
                    {
                        var otMat = ot.Materiales.FirstOrDefault(m => m.id_ot_material == mat.id_ot_material);
                        if (otMat != null)
                            otMat.cantidad_utilizada = mat.cantidad_utilizada;
                    }

                    // Marcar actividades completadas
                    foreach (var idAct in request.ids_actividades_completadas)
                    {
                        var otAct = ot.Actividades.FirstOrDefault(a => a.id_ot_actividad == idAct);
                        if (otAct != null)
                            otAct.estado_ejecucion = "COMPLETADA";
                    }

                    // Registrar corte de PM por cada PM incluido en la OT
                    foreach (var pmDetalle in ot.PlanesDetalle)
                    {
                        await _otRepo.UpsertUltimaIntervencionAsync(new PMUltimaIntervencion
                        {
                            id_equipo        = ot.id_equipo,
                            id_detalle_estrg = pmDetalle.id_detalle_estrg,
                            horometro_corte  = ot.horometro_corte.Value,
                            fecha_corte      = ot.fecha_atencion,
                            id_ot            = ot.id_ot
                        });
                    }

                    // Marcar entradas de calendario como ejecutadas
                    var calItems = await _context.CalendariosMantenimiento
                        .Where(c => c.id_ot == ot.id_ot).ToListAsync();
                    foreach (var cal in calItems)
                    {
                        cal.ejecutado             = true;
                        cal.fecha_real_ejecucion  = DateTime.UtcNow;
                    }
                    await _context.SaveChangesAsync();

                    // Recalcular calendario tras cierre
                    await RecalcularCalendarioAsync(ot.id_equipo);
                }

                await _otRepo.UpdateAsync(ot);
                var full = await _otRepo.GetByIdAsync(idOt);
                return ApiResponse<OrdenTrabajoResponse>.SuccessResult(MapToResponse(full),
                    $"Estado cambiado a {request.nuevo_estado}.");
            }
            catch (Exception ex)
            {
                return ApiResponse<OrdenTrabajoResponse>.Fail($"Error: {ex.Message}");
            }
        }

        // ── EVALUACIÓN AUTOMÁTICA DE UMBRALES ────────────────────
        /// <summary>
        /// Hook llamado desde HistorialHorometroService.CrearAsync().
        /// Evalúa todos los PMs de la estrategia del equipo y genera OTs automáticas
        /// si el horómetro entra en la ventana de tolerancia del PM siguiente.
        /// </summary>
        public async Task EvaluarUmbralesYGenerarOTsAsync(int idEquipo, decimal horometroActual)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Flota)
                        .ThenInclude(f => f.ModeloEquipo)
                            .ThenInclude(m => m.Marca)
                    .FirstOrDefaultAsync(e => e.id_equipo == idEquipo);
                if (equipo == null) return;

                // Buscar estrategia activa para el equipo (individual primero, luego por flota)
                var estrategia = await _context.Estrategias
                    .Include(e => e.Detalles)
                    .FirstOrDefaultAsync(e =>
                        (e.id_equipo == idEquipo || e.id_flota == equipo.id_flota)
                        && e.estado == "Activo");

                if (estrategia == null) return;

                // Buscar planes de mantenimiento activos para la estrategia
                var planes = await _context.PlanesMantenimiento
                    .AsSplitQuery()
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.ActividadSistema)
                            .ThenInclude(s => s.SistemaEquipo)
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.EstrategiaDetalle)
                    .Include(p => p.PlanMantenimientoActividades)
                        .ThenInclude(a => a.Materiales)
                            .ThenInclude(m => m.Material)
                    .Where(p => p.id_estrategia == estrategia.id_estrategia && p.estado)
                    .ToListAsync();

                if (!planes.Any()) return;

                // Evaluar cada PM y recolectar cuáles disparan OT
                var pmsAGenerar = new List<EstrategiaDetalle>();

                foreach (var detalle in estrategia.Detalles)
                {
                    var ultima = await _otRepo.GetUltimaIntervencionAsync(idEquipo, detalle.id_detalle_estrg);
                    decimal horometroCorteBase = ultima?.horometro_corte ?? 0;

                    decimal proximoUmbralInf = horometroCorteBase + detalle.tolerancia_inf;
                    decimal proximoUmbralSup = horometroCorteBase + detalle.tolerancia_sup;

                    bool enVentana = horometroActual >= proximoUmbralInf && horometroActual <= proximoUmbralSup;
                    bool sinOTActiva = !await _otRepo.ExisteOTActivaAsync(idEquipo, detalle.id_detalle_estrg);

                    if (enVentana && sinOTActiva)
                        pmsAGenerar.Add(detalle);
                }

                if (!pmsAGenerar.Any()) return;

                bool otGenerada = false;
                foreach (var detalle in pmsAGenerar)
                {
                    // Buscar el plan que tiene la actividad correspondiente a este detalle
                    var planAsociado = planes.FirstOrDefault(p =>
                        p.PlanMantenimientoActividades.Any(a => a.id_detalle_estrg == detalle.id_detalle_estrg));

                    if (planAsociado != null)
                    {
                        var ot = ConstruirOT(equipo, planAsociado, new List<int> { detalle.id_detalle_estrg }, "PREVENTIVA", "AUTO",
                            horometroActual, null, null);

                        await _otRepo.CreateAsync(ot);
                        otGenerada = true;
                    }
                }

                if (otGenerada)
                {
                    await RecalcularCalendarioAsync(idEquipo);
                }
            }
            catch (Exception ex)
            {
                // No propagamos errores de generación automática para no interrumpir el registro de horómetro, pero lo registramos en consola
                Console.WriteLine($"[AUTO-OT] Error al evaluar/generar OT para Equipo {idEquipo}: {ex.Message} {ex.InnerException?.Message}");
            }
        }

        // ── CALENDARIO ───────────────────────────────────────────
        public async Task RecalcularCalendarioAsync(int idEquipo)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Flota)
                    .FirstOrDefaultAsync(e => e.id_equipo == idEquipo);
                if (equipo == null) return;

                var config = await _otRepo.GetConfiguracionFlotaAsync(equipo.id_flota);
                decimal horasDiarias = config?.horas_diarias_estimadas ?? 12m;

                var estrategia = await _context.Estrategias
                    .Include(e => e.Detalles)
                    .FirstOrDefaultAsync(e =>
                        (e.id_equipo == idEquipo || e.id_flota == equipo.id_flota)
                        && e.estado == "Activo");

                if (estrategia == null) return;

                var ahora = DateTime.UtcNow;

                foreach (var detalle in estrategia.Detalles)
                {
                    var ultima = await _otRepo.GetUltimaIntervencionAsync(idEquipo, detalle.id_detalle_estrg);
                    decimal horometroBase = ultima?.horometro_corte ?? 0;
                    decimal proximoUmbral = horometroBase + detalle.umbral_mant;
                    decimal horasFaltantes = proximoUmbral - equipo.horometro_actual;

                    if (horasFaltantes < 0) horasFaltantes = 0;

                    double diasEstimados = (double)(horasFaltantes / horasDiarias);
                    DateTime fechaEstimada = ahora.AddDays(diasEstimados);

                    await _otRepo.UpsertCalendarioAsync(new CalendarioMantenimiento
                    {
                        id_equipo          = idEquipo,
                        id_detalle_estrg   = detalle.id_detalle_estrg,
                        horometro_base     = horometroBase,
                        fecha_base         = ahora,
                        horas_diarias_usadas = horasDiarias,
                        fecha_estimada     = fechaEstimada,
                        ejecutado          = false
                    });
                }
            }
            catch { /* Silencioso */ }
        }

        public async Task<ApiResponse<CalendarioProyeccionResponse>> GetCalendarioAsync(int idEquipo)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Flota)
                    .FirstOrDefaultAsync(e => e.id_equipo == idEquipo);

                if (equipo == null)
                    return ApiResponse<CalendarioProyeccionResponse>.Fail("Equipo no encontrado.");

                var items = await _otRepo.GetCalendarioByEquipoAsync(idEquipo);

                return ApiResponse<CalendarioProyeccionResponse>.SuccessResult(new CalendarioProyeccionResponse
                {
                    id_equipo        = idEquipo,
                    cod_equipo       = equipo.cod_eqp,
                    placa_equipo     = equipo.placa_eqp,
                    horometro_actual = equipo.horometro_actual,
                    proyecciones     = items.Select(c => new CalendarioPMResponse
                    {
                        id_detalle_estrg    = c.id_detalle_estrg,
                        tipo_pm             = c.EstrategiaDetalle?.tipo_pm,
                        umbral_mant         = c.EstrategiaDetalle?.umbral_mant ?? 0,
                        uni_med             = c.EstrategiaDetalle?.uni_med,
                        horometro_corte_base = c.horometro_base,
                        proximo_umbral      = c.horometro_base + (c.EstrategiaDetalle?.umbral_mant ?? 0),
                        horas_faltantes     = Math.Max(0,
                            c.horometro_base + (c.EstrategiaDetalle?.umbral_mant ?? 0) - equipo.horometro_actual),
                        dias_estimados      = (double)Math.Max(0,
                            (c.horometro_base + (c.EstrategiaDetalle?.umbral_mant ?? 0) - equipo.horometro_actual)
                            / c.horas_diarias_usadas),
                        fecha_estimada      = DateTime.SpecifyKind(c.fecha_estimada, DateTimeKind.Utc),
                        ejecutado           = c.ejecutado,
                        fecha_real_ejecucion = c.fecha_real_ejecucion.HasValue ? DateTime.SpecifyKind(c.fecha_real_ejecucion.Value, DateTimeKind.Utc) : null,
                        id_ot               = c.id_ot,
                        estado_ot           = c.OrdenTrabajo?.estado
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<CalendarioProyeccionResponse>.Fail($"Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<CalendarioProyeccionResponse>>> GetCalendarioFlotaAsync(int idFlota)
        {
            try
            {
                var equipos = await _context.Equipos
                    .Where(e => e.id_flota == idFlota)
                    .ToListAsync();

                var resultado = new List<CalendarioProyeccionResponse>();
                foreach (var eq in equipos)
                {
                    var r = await GetCalendarioAsync(eq.id_equipo);
                    if (r.Success) resultado.Add(r.Data);
                }

                return ApiResponse<IEnumerable<CalendarioProyeccionResponse>>.SuccessResult(resultado);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<CalendarioProyeccionResponse>>.Fail($"Error: {ex.Message}");
            }
        }

        // ── CONFIGURACIÓN FLOTA ──────────────────────────────────
        public async Task<ApiResponse<ConfiguracionFlotaResponse>> SetConfiguracionFlotaAsync(ConfiguracionFlotaRequest request)
        {
            try
            {
                var config = new ConfiguracionFlota
                {
                    id_flota                 = request.id_flota,
                    horas_diarias_estimadas  = request.horas_diarias_estimadas,
                    fecha_actualizacion      = DateTime.UtcNow,
                    actualizado_por          = request.actualizado_por
                };

                await _otRepo.UpsertConfiguracionFlotaAsync(config);

                return ApiResponse<ConfiguracionFlotaResponse>.SuccessResult(new ConfiguracionFlotaResponse
                {
                    id_flota                 = config.id_flota,
                    horas_diarias_estimadas  = config.horas_diarias_estimadas,
                    fecha_actualizacion      = DateTime.SpecifyKind(config.fecha_actualizacion, DateTimeKind.Utc)
                }, "Configuración guardada.");
            }
            catch (Exception ex)
            {
                return ApiResponse<ConfiguracionFlotaResponse>.Fail($"Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfiguracionFlotaResponse>> GetConfiguracionFlotaAsync(int? idFlota)
        {
            var cfg = await _otRepo.GetConfiguracionFlotaAsync(idFlota);
            if (cfg == null)
                return ApiResponse<ConfiguracionFlotaResponse>.Fail("Sin configuración definida.");

            return ApiResponse<ConfiguracionFlotaResponse>.SuccessResult(new ConfiguracionFlotaResponse
            {
                id_configuracion         = cfg.id_configuracion,
                id_flota                 = cfg.id_flota,
                nombre_flota             = cfg.Flota?.nombre_flota,
                horas_diarias_estimadas  = cfg.horas_diarias_estimadas,
                fecha_actualizacion      = DateTime.SpecifyKind(cfg.fecha_actualizacion, DateTimeKind.Utc)
            });
        }

        // ── HELPERS PRIVADOS ─────────────────────────────────────

        private static void ValidarTransicionEstado(string estadoActual, string nuevoEstado)
        {
            var trimmedActual = estadoActual?.Trim().ToUpper() ?? string.Empty;
            var trimmedNuevo  = nuevoEstado?.Trim().ToUpper() ?? string.Empty;

            var transicionesValidas = new Dictionary<string, List<string>>
            {
                ["PENDIENTE"]   = new List<string> { "ACTIVA", "INACTIVA" },
                ["ACTIVA"]      = new List<string> { "EN_REVISION", "INACTIVA" },
                ["EN_REVISION"] = new List<string> { "CERRADA", "ACTIVA" },
                ["CERRADA"]     = new List<string>(),
                ["INACTIVA"]    = new List<string>()
            };

            if (!transicionesValidas.ContainsKey(trimmedActual) ||
                !transicionesValidas[trimmedActual].Contains(trimmedNuevo))
                throw new InvalidOperationException(
                    $"Transición de estado no permitida: {trimmedActual} → {trimmedNuevo}");
        }

        private static string GenerarCodOT()
        {
            return $"OT-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }

        /// <summary>Construye una OT con sus actividades y materiales copiados del plan.</summary>
        private OrdenTrabajo ConstruirOT(
            ApiMantenimiento.Models.Entitys.MFlota.Equipo equipo,
            PlanMantenimiento plan,
            List<int> idsDetalle,
            string tipoOT,
            string formaGeneracion,
            decimal horometroActual,
            string observaciones,
            string creadoPor)
        {
            var ot = new OrdenTrabajo
            {
                cod_ot              = GenerarCodOT(),
                id_equipo           = equipo.id_equipo,
                id_plan_mant        = plan.id_plan_mant,
                tipo_ot             = tipoOT,
                forma_generacion    = formaGeneracion,
                estado              = "PENDIENTE",
                horometro_al_momento = horometroActual,
                fecha_creacion      = DateTime.UtcNow,
                observaciones       = observaciones,
                creado_por          = creadoPor
            };

            // PMs incluidos
            foreach (var idDet in idsDetalle)
                ot.PlanesDetalle.Add(new OTPlanDetalle { id_detalle_estrg = idDet });

            // Actividades (snapshot) — solo las que pertenecen a los PMs incluidos
            var actividadesFiltradas = plan.PlanMantenimientoActividades
                .Where(a => idsDetalle.Contains(a.id_detalle_estrg))
                .ToList();

            foreach (var act in actividadesFiltradas)
            {
                ot.Actividades.Add(new OTActividad
                {
                    id_actividad_ref = act.id_actividad,
                    nombre_actividad = act.ActividadSistema?.nombre_actividad ?? "Sin nombre",
                    cod_sistema      = act.ActividadSistema?.SistemaEquipo?.cod_sist ?? string.Empty,
                    tipo_pm          = act.EstrategiaDetalle?.tipo_pm ?? string.Empty,
                    observacion_tecnica = string.Empty,
                    estado_ejecucion = "PENDIENTE"
                });

                // Material (snapshot) — máx. 1 por actividad
                var mat = act.Materiales?.FirstOrDefault();
                if (mat?.Material != null)
                {
                    ot.Materiales.Add(new OTMaterial
                    {
                        id_material_ref    = mat.id_material,
                        cod_materia        = mat.Material.cod_materia ?? string.Empty,
                        descripcion_material = mat.Material.descripcion,
                        cantidad_requerida = mat.cantidad
                    });
                }
            }

            return ot;
        }

        // ── MAPPER ───────────────────────────────────────────────
        private static OrdenTrabajoResponse MapToResponse(OrdenTrabajo ot)
        {
            return new OrdenTrabajoResponse
            {
                id_ot                = ot.id_ot,
                cod_ot               = ot.cod_ot,
                tipo_ot              = ot.tipo_ot,
                forma_generacion     = ot.forma_generacion,
                estado               = ot.estado,
                horometro_al_momento = ot.horometro_al_momento,
                horometro_corte      = ot.horometro_corte,
                fecha_creacion       = DateTime.SpecifyKind(ot.fecha_creacion, DateTimeKind.Utc),
                fecha_atencion       = ot.fecha_atencion.HasValue ? DateTime.SpecifyKind(ot.fecha_atencion.Value, DateTimeKind.Utc) : null,
                observaciones        = ot.observaciones,
                creado_por           = ot.creado_por,

                hora_intervencion    = ot.hora_intervencion.HasValue ? DateTime.SpecifyKind(ot.hora_intervencion.Value, DateTimeKind.Utc) : null,
                descripcion_falla    = ot.descripcion_falla,
                id_sistema           = ot.id_sistema,
                nombre_sistema       = ot.Sistema?.nombre_sist,
                id_subsistema        = ot.id_subsistema,
                nombre_subsistema    = ot.SubSistema?.nombre_subsist,
                horometro_falla      = ot.horometro_falla,

                id_equipo    = ot.id_equipo,
                cod_equipo   = ot.Equipo?.cod_eqp,
                placa_equipo = ot.Equipo?.placa_eqp,
                num_serie    = ot.Equipo?.num_serie,
                nombre_flota = ot.Equipo?.Flota?.nombre_flota,
                marca        = ot.Equipo?.Flota?.ModeloEquipo?.Marca?.nombre_marca,
                modelo       = ot.Equipo?.Flota?.ModeloEquipo?.nomb_modelo,

                id_plan_mant       = ot.id_plan_mant,
                titulo_estrategia  = ot.PlanMantenimiento?.Estrategia?.titulo_estrategia,

                pms_incluidos = ot.PlanesDetalle?.Select(d => new OTPlanDetalleResponse
                {
                    id_detalle_estrg = d.id_detalle_estrg,
                    tipo_pm          = d.EstrategiaDetalle?.tipo_pm,
                    umbral_mant      = d.EstrategiaDetalle?.umbral_mant ?? 0,
                    uni_med          = d.EstrategiaDetalle?.uni_med
                }).ToList() ?? new(),

                actividades = ot.Actividades?.Select(a => new OTActividadResponse
                {
                    id_ot_actividad    = a.id_ot_actividad,
                    nombre_actividad   = a.nombre_actividad,
                    cod_sistema        = a.cod_sistema,
                    tipo_pm            = a.tipo_pm,
                    estado_ejecucion   = a.estado_ejecucion,
                    observacion_tecnica = a.observacion_tecnica
                }).ToList() ?? new(),

                materiales = ot.Materiales?.Select(m => new OTMaterialResponse
                {
                    id_ot_material       = m.id_ot_material,
                    cod_materia          = m.cod_materia,
                    descripcion_material = m.descripcion_material,
                    cantidad_requerida   = m.cantidad_requerida,
                    cantidad_utilizada   = m.cantidad_utilizada
                }).ToList() ?? new(),

                personal = ot.Personal?.Select(p => new OTPersonalResponse
                {
                    id_ot_personal = p.id_ot_personal,
                    dni_empleado   = p.dni_empleado,
                    nombre_empleado = p.Empleado != null
                        ? $"{p.Empleado.nombre} {p.Empleado.apellido1}"
                        : p.dni_empleado,
                    rol = p.Empleado?.Rol?.nombre_rol
                }).ToList() ?? new()
            };
        }

        public async Task<ApiResponse<OTActividadResponse>> AddActividadExtraAsync(int idOt, AgregarActividadOTRequest request)
        {
            try
            {
                var ot = await _context.OrdenesTrabajoMant.FirstOrDefaultAsync(o => o.id_ot == idOt);
                if (ot == null)
                    return ApiResponse<OTActividadResponse>.Fail("Orden de Trabajo no encontrada.");

                if (ot.estado == "CERRADA" || ot.estado == "INACTIVA")
                    return ApiResponse<OTActividadResponse>.Fail("No se pueden agregar actividades a una OT cerrada o anulada.");

                var actividad = new OTActividad
                {
                    id_ot = idOt,
                    id_actividad_ref = null,
                    nombre_actividad = request.nombre_actividad,
                    cod_sistema = request.cod_sistema ?? string.Empty,
                    tipo_pm = string.IsNullOrEmpty(request.tipo_pm) ? "General" : request.tipo_pm,
                    estado_ejecucion = "PENDIENTE",
                    observacion_tecnica = string.Empty
                };

                await _context.OTActividades.AddAsync(actividad);
                await _context.SaveChangesAsync();

                var response = new OTActividadResponse
                {
                    id_ot_actividad = actividad.id_ot_actividad,
                    nombre_actividad = actividad.nombre_actividad,
                    cod_sistema = actividad.cod_sistema,
                    tipo_pm = actividad.tipo_pm,
                    estado_ejecucion = actividad.estado_ejecucion,
                    observacion_tecnica = actividad.observacion_tecnica
                };

                return ApiResponse<OTActividadResponse>.SuccessResult(response, "Actividad adicional agregada con éxito.");
            }
            catch (Exception ex)
            {
                return ApiResponse<OTActividadResponse>.Fail($"Error al agregar actividad: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> RemoveActividadExtraAsync(int idOtActividad)
        {
            try
            {
                var actividad = await _context.OTActividades
                    .Include(a => a.OrdenTrabajo)
                    .FirstOrDefaultAsync(a => a.id_ot_actividad == idOtActividad);

                if (actividad == null)
                    return ApiResponse<bool>.Fail("Actividad no encontrada.");

                if (actividad.OrdenTrabajo?.estado == "CERRADA" || actividad.OrdenTrabajo?.estado == "INACTIVA")
                    return ApiResponse<bool>.Fail("No se pueden remover actividades de una OT cerrada o anulada.");

                _context.OTActividades.Remove(actividad);
                await _context.SaveChangesAsync();

                return ApiResponse<bool>.SuccessResult(true, "Actividad removida con éxito.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"Error al remover actividad: {ex.Message}");
            }
        }

        public async Task<ApiResponse<OTMaterialResponse>> AddMaterialExtraAsync(int idOt, AgregarMaterialOTRequest request)
        {
            try
            {
                var ot = await _context.OrdenesTrabajoMant.FirstOrDefaultAsync(o => o.id_ot == idOt);
                if (ot == null)
                    return ApiResponse<OTMaterialResponse>.Fail("Orden de Trabajo no encontrada.");

                if (ot.estado == "CERRADA" || ot.estado == "INACTIVA")
                    return ApiResponse<OTMaterialResponse>.Fail("No se pueden agregar materiales a una OT cerrada o anulada.");

                var material = await _context.Materiales.FirstOrDefaultAsync(m => m.id_material == request.id_material_ref);
                if (material == null)
                    return ApiResponse<OTMaterialResponse>.Fail("Material no encontrado en el catálogo.");

                var otMaterial = new OTMaterial
                {
                    id_ot = idOt,
                    id_material_ref = request.id_material_ref,
                    cod_materia = material.cod_materia,
                    descripcion_material = material.descripcion,
                    cantidad_requerida = request.cantidad_requerida,
                    cantidad_utilizada = null
                };

                await _context.OTMateriales.AddAsync(otMaterial);
                await _context.SaveChangesAsync();

                var response = new OTMaterialResponse
                {
                    id_ot_material = otMaterial.id_ot_material,
                    cod_materia = otMaterial.cod_materia,
                    descripcion_material = otMaterial.descripcion_material,
                    cantidad_requerida = otMaterial.cantidad_requerida,
                    cantidad_utilizada = otMaterial.cantidad_utilizada
                };

                return ApiResponse<OTMaterialResponse>.SuccessResult(response, "Material adicional agregado con éxito.");
            }
            catch (Exception ex)
            {
                return ApiResponse<OTMaterialResponse>.Fail($"Error al agregar material: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> RemoveMaterialExtraAsync(int idOtMaterial)
        {
            try
            {
                var otMaterial = await _context.OTMateriales
                    .Include(m => m.OrdenTrabajo)
                    .FirstOrDefaultAsync(m => m.id_ot_material == idOtMaterial);

                if (otMaterial == null)
                    return ApiResponse<bool>.Fail("Material no encontrado.");

                if (otMaterial.OrdenTrabajo?.estado == "CERRADA" || otMaterial.OrdenTrabajo?.estado == "INACTIVA")
                    return ApiResponse<bool>.Fail("No se pueden remover materiales de una OT cerrada o anulada.");

                _context.OTMateriales.Remove(otMaterial);
                await _context.SaveChangesAsync();

                return ApiResponse<bool>.SuccessResult(true, "Material removido con éxito.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"Error al remover material: {ex.Message}");
            }
        }

        public async Task<ApiResponse<OTPersonalResponse>> AddPersonalExtraAsync(int idOt, AgregarPersonalOTRequest request)
        {
            try
            {
                var ot = await _context.OrdenesTrabajoMant.FirstOrDefaultAsync(o => o.id_ot == idOt);
                if (ot == null)
                    return ApiResponse<OTPersonalResponse>.Fail("Orden de Trabajo no encontrada.");

                if (ot.estado == "CERRADA" || ot.estado == "INACTIVA")
                    return ApiResponse<OTPersonalResponse>.Fail("No se puede asignar personal a una OT cerrada o anulada.");

                var empleado = await _context.Empleados
                    .Include(e => e.Rol)
                    .FirstOrDefaultAsync(e => e.dni_empleado == request.dni_empleado);
                if (empleado == null)
                    return ApiResponse<OTPersonalResponse>.Fail("Empleado no encontrado.");

                var nombreRol = empleado.Rol?.nombre_rol ?? string.Empty;
                if (!nombreRol.Contains("Tecnico", StringComparison.OrdinalIgnoreCase) && 
                    !nombreRol.Contains("Técnico", StringComparison.OrdinalIgnoreCase))
                {
                    return ApiResponse<OTPersonalResponse>.Fail("El empleado no posee un rol de Técnico.");
                }

                var existe = await _context.OTPersonal.AnyAsync(p => p.id_ot == idOt && p.dni_empleado == request.dni_empleado);
                if (existe)
                    return ApiResponse<OTPersonalResponse>.Fail("El empleado ya está asignado a esta orden de trabajo.");

                var fechaInicio = ot.fecha_creacion.Date;
                var fechaFin = fechaInicio.AddDays(1);
                var count = await _context.OTPersonal
                    .CountAsync(p => p.dni_empleado == request.dni_empleado 
                                     && p.OrdenTrabajo.fecha_creacion >= fechaInicio 
                                     && p.OrdenTrabajo.fecha_creacion < fechaFin
                                     && p.OrdenTrabajo.estado != "INACTIVA");
                if (count >= 2)
                {
                    return ApiResponse<OTPersonalResponse>.Fail("El técnico ya tiene el límite de 2 órdenes de trabajo asignadas para este día.");
                }

                var otPersonal = new OTPersonal
                {
                    id_ot = idOt,
                    dni_empleado = request.dni_empleado,
                    id_rol = empleado.id_rol
                };

                await _context.OTPersonal.AddAsync(otPersonal);
                await _context.SaveChangesAsync();

                var response = new OTPersonalResponse
                {
                    id_ot_personal = otPersonal.id_ot_personal,
                    dni_empleado = otPersonal.dni_empleado,
                    nombre_empleado = $"{empleado.nombre} {empleado.apellido1}",
                    rol = nombreRol
                };

                return ApiResponse<OTPersonalResponse>.SuccessResult(response, "Personal técnico asignado con éxito.");
            }
            catch (Exception ex)
            {
                return ApiResponse<OTPersonalResponse>.Fail($"Error al asignar personal: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> RemovePersonalExtraAsync(int idOtPersonal)
        {
            try
            {
                var otPersonal = await _context.OTPersonal
                    .Include(p => p.OrdenTrabajo)
                    .FirstOrDefaultAsync(p => p.id_ot_personal == idOtPersonal);

                if (otPersonal == null)
                    return ApiResponse<bool>.Fail("Asignación de personal no encontrada.");

                if (otPersonal.OrdenTrabajo?.estado == "CERRADA" || otPersonal.OrdenTrabajo?.estado == "INACTIVA")
                    return ApiResponse<bool>.Fail("No se puede remover personal de una OT cerrada o anulada.");

                _context.OTPersonal.Remove(otPersonal);
                await _context.SaveChangesAsync();

                return ApiResponse<bool>.SuccessResult(true, "Personal técnico removido con éxito.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail($"Error al remover personal: {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<EmpleadoDisponibleResponse>>> GetTecnicosDisponiblesAsync(int idOt)
        {
            try
            {
                var ot = await _context.OrdenesTrabajoMant.FirstOrDefaultAsync(o => o.id_ot == idOt);
                if (ot == null)
                    return ApiResponse<IEnumerable<EmpleadoDisponibleResponse>>.Fail("Orden de Trabajo no encontrada.");

                var fechaInicio = ot.fecha_creacion.Date;
                var fechaFin = fechaInicio.AddDays(1);

                var empleados = await _context.Empleados
                    .Include(e => e.Rol)
                    .Where(e => e.estado == true)
                    .ToListAsync();

                var tecnicos = empleados.Where(e => 
                    e.Rol != null && 
                    (e.Rol.nombre_rol.Contains("Tecnico", StringComparison.OrdinalIgnoreCase) || 
                     e.Rol.nombre_rol.Contains("Técnico", StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                var asignacionesDelDia = await _context.OTPersonal
                    .Include(p => p.OrdenTrabajo)
                    .Where(p => p.OrdenTrabajo.fecha_creacion >= fechaInicio 
                                && p.OrdenTrabajo.fecha_creacion < fechaFin
                                && p.OrdenTrabajo.estado != "INACTIVA")
                    .ToListAsync();

                var conteoPorEmpleado = asignacionesDelDia
                    .GroupBy(p => p.dni_empleado)
                    .ToDictionary(g => g.Key, g => g.Count());

                var listado = tecnicos.Select(e => {
                    conteoPorEmpleado.TryGetValue(e.dni_empleado, out int count);
                    bool disponible = count < 2;
                    string motivo = disponible ? null : "Límite de 2 OTs diarias alcanzado";
                    return new EmpleadoDisponibleResponse
                    {
                        dni_empleado = e.dni_empleado,
                        codigo_empleado = e.codigo_empleado,
                        nombre = e.nombre,
                        apellido1 = e.apellido1,
                        apellido2 = e.apellido2,
                        id_rol = e.id_rol,
                        nombreRol = e.Rol?.nombre_rol,
                        disponible = disponible,
                        motivo_no_disponible = motivo,
                        ots_hoy = count
                    };
                }).ToList();

                return ApiResponse<IEnumerable<EmpleadoDisponibleResponse>>.SuccessResult(listado);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<EmpleadoDisponibleResponse>>.Fail($"Error al obtener técnicos: {ex.Message}");
            }
        }
    }
}
