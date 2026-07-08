using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MAlmacen
{
    public class ValeRepository : IValeRepository
    {
        private readonly MantenimientoDbContext _context;

        public ValeRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<Vale?> GetByIdAsync(int id)
        {
            return await _context.Vales
                .Include(v => v.OrdenTrabajo)
                    .ThenInclude(ot => ot.Equipo)
                .Include(v => v.Materiales)
                    .ThenInclude(vm => vm.Material)
                .FirstOrDefaultAsync(v => v.id_vale == id);
        }

        public async Task<Vale?> GetByOtIdAsync(int idOt)
        {
            return await _context.Vales
                .Include(v => v.OrdenTrabajo)
                .FirstOrDefaultAsync(v => v.id_ot == idOt);
        }

        public async Task<IEnumerable<Vale>> GetAllAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, string? search)
        {
            var query = _context.Vales
                .Include(v => v.OrdenTrabajo)
                .Include(v => v.Materiales)
                    .ThenInclude(vm => vm.Material)
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(v => v.estado == estado);
            }

            if (fechaInicio.HasValue)
            {
                query = query.Where(v => v.fecha_creacion >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                // Incluye todo el día hasta las 23:59:59
                var finDia = fechaFin.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(v => v.fecha_creacion <= finDia);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(v => 
                    v.cod_vale.ToLower().Contains(lowerSearch) ||
                    v.solicitado_por.ToLower().Contains(lowerSearch) ||
                    (v.OrdenTrabajo != null && v.OrdenTrabajo.cod_ot.ToLower().Contains(lowerSearch))
                );
            }

            return await query.OrderByDescending(v => v.fecha_creacion).ToListAsync();
        }

        public async Task<Vale> AddAsync(Vale vale)
        {
            await _context.Vales.AddAsync(vale);
            await _context.SaveChangesAsync();
            return vale;
        }

        public async Task UpdateAsync(Vale vale)
        {
            _context.Vales.Update(vale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vale vale)
        {
            _context.Vales.Remove(vale);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservedMaterialResponse>> GetReservedMaterialsAsync()
        {
            return await _context.Vales
                .Where(v => v.estado == "PENDIENTE")
                .SelectMany(v => v.Materiales)
                .GroupBy(vm => new { vm.id_material, vm.Material.cod_materia, vm.Material.descripcion })
                .Select(g => new ReservedMaterialResponse
                {
                    id_material = g.Key.id_material,
                    cod_materia = g.Key.cod_materia,
                    descripcion = g.Key.descripcion,
                    cantidad_reservada = g.Sum(vm => vm.cantidad_solicitada)
                })
                .ToListAsync();
        }
    }
}
