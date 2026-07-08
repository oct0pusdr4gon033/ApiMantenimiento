using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MMantenimiento
{
    public class PlanMantenimientoRepository : IPlanMantenimientoRepository
    {
        private readonly MantenimientoDbContext _context;

        public PlanMantenimientoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanMantenimiento>> GetAllAsync()
        {
            return await _context.PlanesMantenimiento
                .AsSplitQuery()
                .Include(p => p.Estrategia)
                    .ThenInclude(e => e.Flota)
                .Include(p => p.Estrategia)
                    .ThenInclude(e => e.Equipo)
                .Include(p => p.Estrategia)
                    .ThenInclude(e => e.Detalles)
                .Include(p => p.PlanMantenimientoActividades)
                    .ThenInclude(a => a.ActividadSistema)
                        .ThenInclude(s => s.SistemaEquipo)
                .Include(p => p.PlanMantenimientoActividades)
                    .ThenInclude(a => a.EstrategiaDetalle)
                .Include(p => p.PlanMantenimientoActividades)
                    .ThenInclude(a => a.Materiales)   // nueva tabla 3FN
                        .ThenInclude(m => m.Material)
                .Include(p => p.PlanMantenimientoPersonales)
                    .ThenInclude(per => per.Rol)
                .ToListAsync();
        }

        public async Task<PlanMantenimiento> GetByIdAsync(int id)
        {
            return await _context.PlanesMantenimiento
                .AsSplitQuery()
                .Include(p => p.Estrategia)
                    .ThenInclude(e => e.Flota)
                .Include(p => p.Estrategia)
                    .ThenInclude(e => e.Equipo)
                .Include(p => p.Estrategia)
                    .ThenInclude(e => e.Detalles)
                .Include(p => p.PlanMantenimientoActividades)
                    .ThenInclude(a => a.ActividadSistema)
                        .ThenInclude(s => s.SistemaEquipo)
                .Include(p => p.PlanMantenimientoActividades)
                    .ThenInclude(a => a.EstrategiaDetalle)
                .Include(p => p.PlanMantenimientoActividades)
                    .ThenInclude(a => a.Materiales)   // nueva tabla 3FN
                        .ThenInclude(m => m.Material)
                .Include(p => p.PlanMantenimientoPersonales)
                    .ThenInclude(per => per.Rol)
                .FirstOrDefaultAsync(p => p.id_plan_mant == id);
        }

        public async Task<PlanMantenimiento> CreateAsync(PlanMantenimiento plan)
        {
            await _context.PlanesMantenimiento.AddAsync(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task UpdateAsync(PlanMantenimiento plan)
        {
            _context.PlanesMantenimiento.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.PlanesMantenimiento.FindAsync(id);
            if (plan != null)
            {
                _context.PlanesMantenimiento.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }

        // ── Actividades ──────────────────────────────────────────────
        public async Task AddActividadAsync(PlanMantenimientoActividad actividad)
        {
            await _context.PlanesMantenimientoActividades.AddAsync(actividad);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una actividad identificada por su clave compuesta.
        /// </summary>
        public async Task RemoveActividadAsync(int id_plan_mant, int id_actividad, int id_detalle_estrg)
        {
            var entity = await _context.PlanesMantenimientoActividades
                .FindAsync(id_plan_mant, id_actividad, id_detalle_estrg);
            if (entity != null)
            {
                _context.PlanesMantenimientoActividades.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ── Personal ──────────────────────────────────────────────────
        public async Task AddPersonalAsync(PlanMantenimientoPersonal personal)
        {
            await _context.PlanesMantenimientoPersonales.AddAsync(personal);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePersonalAsync(int id_plan_personal)
        {
            var entity = await _context.PlanesMantenimientoPersonales.FindAsync(id_plan_personal);
            if (entity != null)
            {
                _context.PlanesMantenimientoPersonales.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
