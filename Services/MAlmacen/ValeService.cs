using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using ApiMantenimiento.Services.Interfaces.MAlmacen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MAlmacen
{
    public class ValeService : IValeService
    {
        private readonly IValeRepository _valeRepo;
        private readonly IMovimientoInventarioRepository _movRepo;
        private readonly MantenimientoDbContext _context;

        public ValeService(IValeRepository valeRepo, IMovimientoInventarioRepository movRepo, MantenimientoDbContext context)
        {
            _valeRepo = valeRepo;
            _movRepo = movRepo;
            _context = context;
        }

        public async Task<ValeResponse> GetByIdAsync(int id)
        {
            var vale = await _valeRepo.GetByIdAsync(id);
            if (vale == null)
            {
                throw new KeyNotFoundException($"No se encontró el Vale con ID {id}");
            }
            return MapToResponse(vale);
        }

        public async Task<ValeResponse?> GetByOtIdAsync(int idOt)
        {
            var vale = await _valeRepo.GetByOtIdAsync(idOt);
            if (vale == null) return null;
            return MapToResponse(vale);
        }

        public async Task<IEnumerable<ValeResponse>> GetAllAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, string? search)
        {
            var vales = await _valeRepo.GetAllAsync(estado, fechaInicio, fechaFin, search);
            return vales.Select(MapToResponse);
        }

        public async Task<ValeResponse> CreateAsync(ValeCreateRequest request)
        {
            // Validar relación 1-1 con OT si se especifica
            if (request.id_ot.HasValue)
            {
                var otExiste = await _context.OrdenesTrabajoMant.AnyAsync(o => o.id_ot == request.id_ot.Value);
                if (!otExiste)
                {
                    throw new ArgumentException($"La Orden de Trabajo con ID {request.id_ot.Value} no existe.");
                }

                var valeExistente = await _valeRepo.GetByOtIdAsync(request.id_ot.Value);
                if (valeExistente != null)
                {
                    throw new InvalidOperationException($"La Orden de Trabajo ya tiene asociado el Vale {valeExistente.cod_vale}.");
                }
            }

            // Generar código único correlativo VAL-XXXXX
            var nextCode = await GenerarSiguienteCodigoAsync();

            var vale = new Vale
            {
                cod_vale = nextCode,
                id_ot = request.id_ot,
                estado = "PENDIENTE",
                fecha_creacion = DateTime.Now,
                solicitado_por = request.solicitado_por,
                observaciones = request.observaciones
            };

            foreach (var matReq in request.materiales)
            {
                var material = await _context.Materiales.FindAsync(matReq.id_material);
                if (material == null)
                {
                    throw new KeyNotFoundException($"El Material con ID {matReq.id_material} no existe.");
                }

                vale.Materiales.Add(new ValeMaterial
                {
                    id_material = matReq.id_material,
                    cantidad_solicitada = matReq.cantidad_solicitada
                });
            }

            var result = await _valeRepo.AddAsync(vale);
            
            // Recargar con navegación para el mapeo
            var valeCompleto = await _valeRepo.GetByIdAsync(result.id_vale);
            return MapToResponse(valeCompleto!);
        }

        public async Task<ValeResponse> UpdateAsync(int id, ValeUpdateRequest request)
        {
            var vale = await _valeRepo.GetByIdAsync(id);
            if (vale == null)
            {
                throw new KeyNotFoundException($"No se encontró el Vale con ID {id}");
            }

            if (vale.estado == "DESPACHADO")
            {
                throw new InvalidOperationException("No se puede editar un vale que ya ha sido DESPACHADO.");
            }

            vale.solicitado_por = request.solicitado_por;
            vale.observaciones = request.observaciones;

            // Eliminar detalles anteriores
            _context.ValesMateriales.RemoveRange(vale.Materiales);
            vale.Materiales.Clear();

            // Agregar nuevos detalles
            foreach (var matReq in request.materiales)
            {
                var material = await _context.Materiales.FindAsync(matReq.id_material);
                if (material == null)
                {
                    throw new KeyNotFoundException($"El Material con ID {matReq.id_material} no existe.");
                }

                vale.Materiales.Add(new ValeMaterial
                {
                    id_material = matReq.id_material,
                    cantidad_solicitada = matReq.cantidad_solicitada
                });
            }

            await _valeRepo.UpdateAsync(vale);

            var valeCompleto = await _valeRepo.GetByIdAsync(vale.id_vale);
            return MapToResponse(valeCompleto!);
        }

        public async Task DeleteAsync(int id)
        {
            var vale = await _valeRepo.GetByIdAsync(id);
            if (vale == null)
            {
                throw new KeyNotFoundException($"No se encontró el Vale con ID {id}");
            }

            if (vale.estado == "DESPACHADO")
            {
                throw new InvalidOperationException("No se puede eliminar un vale que ya ha sido DESPACHADO.");
            }

            await _valeRepo.DeleteAsync(vale);
        }

        public async Task<ValeResponse> DispatchAsync(int id, ValeDispatchRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var vale = await _valeRepo.GetByIdAsync(id);
                if (vale == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Vale con ID {id}");
                }

                if (vale.estado == "DESPACHADO")
                {
                    throw new InvalidOperationException("El vale ya ha sido despachado anteriormente.");
                }

                foreach (var dispItem in request.materiales)
                {
                    var valeMaterial = vale.Materiales.FirstOrDefault(vm => vm.id_vale_material == dispItem.id_vale_material);
                    if (valeMaterial == null)
                    {
                        throw new KeyNotFoundException($"No se encontró el detalle de material con ID {dispItem.id_vale_material} en este vale.");
                    }

                    var material = await _context.Materiales.FindAsync(valeMaterial.id_material);
                    if (material == null)
                    {
                        throw new KeyNotFoundException($"No se encontró el Material maestro con ID {valeMaterial.id_material}");
                    }

                    // Validar stock
                    if (material.stock < dispItem.cantidad_despachada)
                    {
                        throw new InvalidOperationException($"Stock insuficiente para el material '{material.descripcion}'. Solicitado/Despachado: {dispItem.cantidad_despachada:0.00}, Disponible: {material.stock:0.00}");
                    }

                    // Decrementar stock
                    material.stock -= dispItem.cantidad_despachada;
                    valeMaterial.cantidad_despachada = dispItem.cantidad_despachada;

                    // Registrar SALIDA en Kardex
                    var movimiento = new MovimientoInventario
                    {
                        id_material = material.id_material,
                        fecha = DateTime.Now,
                        tipo_movimiento = "SALIDA",
                        cantidad = dispItem.cantidad_despachada,
                        saldo_stock = material.stock,
                        origen_tipo = "NOTA_SALIDA",
                        origen_referencia = vale.cod_vale,
                        responsable = request.despachado_por,
                        observaciones = $"Salida por vale {vale.cod_vale}."
                    };

                    await _movRepo.AddAsync(movimiento);
                }

                // Cambiar estado a DESPACHADO
                vale.estado = "DESPACHADO";
                vale.fecha_despacho = DateTime.Now;
                vale.despachado_por = request.despachado_por;

                await _valeRepo.UpdateAsync(vale);
                await transaction.CommitAsync();

                var valeCompleto = await _valeRepo.GetByIdAsync(vale.id_vale);
                return MapToResponse(valeCompleto!);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ReservedMaterialResponse>> GetReservedMaterialsAsync()
        {
            return await _valeRepo.GetReservedMaterialsAsync();
        }

        // Métodos Auxiliares
        private async Task<string> GenerarSiguienteCodigoAsync()
        {
            var lastVale = await _context.Vales.OrderByDescending(v => v.id_vale).FirstOrDefaultAsync();
            int nextNum = 1;

            if (lastVale != null && lastVale.cod_vale.StartsWith("VAL-"))
            {
                if (int.TryParse(lastVale.cod_vale.Replace("VAL-", ""), out int num))
                {
                    nextNum = num + 1;
                }
            }

            return $"VAL-{nextNum:D5}";
        }

        private ValeResponse MapToResponse(Vale vale)
        {
            return new ValeResponse
            {
                id_vale = vale.id_vale,
                cod_vale = vale.cod_vale,
                id_ot = vale.id_ot,
                cod_ot = vale.OrdenTrabajo?.cod_ot,
                cod_equipo = vale.OrdenTrabajo?.Equipo?.cod_eqp,
                estado = vale.estado,
                fecha_creacion = vale.fecha_creacion,
                fecha_despacho = vale.fecha_despacho,
                solicitado_por = vale.solicitado_por,
                despachado_por = vale.despachado_por,
                observaciones = vale.observaciones,
                materiales = vale.Materiales.Select(vm => new ValeMaterialResponse
                {
                    id_vale_material = vm.id_vale_material,
                    id_material = vm.id_material,
                    cod_materia = vm.Material?.cod_materia ?? "",
                    descripcion = vm.Material?.descripcion ?? "",
                    cantidad_solicitada = vm.cantidad_solicitada,
                    cantidad_despachada = vm.cantidad_despachada
                }).ToList()
            };
        }
    }
}
