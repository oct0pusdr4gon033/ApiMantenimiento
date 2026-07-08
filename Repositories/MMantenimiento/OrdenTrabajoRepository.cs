using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MMantenimiento
{
    public class OrdenTrabajoRepository : IOrdenTrabajoRepository
    {
        private readonly MantenimientoDbContext _context;

        public OrdenTrabajoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        // ── OT ────────────────────────────────────────────────────

        public async Task<IEnumerable<OrdenTrabajo>> GetAllAsync()
        {
            return await _context.OrdenesTrabajoMant
                .AsSplitQuery()
                .Include(o => o.Equipo).ThenInclude(e => e.Flota)
                    .ThenInclude(f => f.ModeloEquipo).ThenInclude(m => m.Marca)
                .Include(o => o.PlanMantenimiento).ThenInclude(p => p.Estrategia)
                .Include(o => o.PlanesDetalle).ThenInclude(d => d.EstrategiaDetalle)
                .Include(o => o.Actividades)
                .Include(o => o.Materiales)
                .Include(o => o.Personal).ThenInclude(p => p.Empleado)
                .Include(o => o.Sistema)
                .Include(o => o.SubSistema)
                .OrderByDescending(o => o.fecha_creacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrdenTrabajo>> GetByEquipoAsync(int idEquipo)
        {
            return await _context.OrdenesTrabajoMant
                .AsSplitQuery()
                .Include(o => o.Equipo).ThenInclude(e => e.Flota)
                    .ThenInclude(f => f.ModeloEquipo).ThenInclude(m => m.Marca)
                .Include(o => o.PlanMantenimiento).ThenInclude(p => p.Estrategia)
                .Include(o => o.PlanesDetalle).ThenInclude(d => d.EstrategiaDetalle)
                .Include(o => o.Actividades)
                .Include(o => o.Materiales)
                .Include(o => o.Personal).ThenInclude(p => p.Empleado).ThenInclude(e => e.Rol)
                .Include(o => o.Sistema)
                .Include(o => o.SubSistema)
                .Where(o => o.id_equipo == idEquipo)
                .OrderByDescending(o => o.fecha_creacion)
                .ToListAsync();
        }

        public async Task<OrdenTrabajo> GetByIdAsync(int idOt)
        {
            return await _context.OrdenesTrabajoMant
                .AsSplitQuery()
                .Include(o => o.Equipo).ThenInclude(e => e.Flota)
                    .ThenInclude(f => f.ModeloEquipo).ThenInclude(m => m.Marca)
                .Include(o => o.PlanMantenimiento).ThenInclude(p => p.Estrategia)
                .Include(o => o.PlanesDetalle).ThenInclude(d => d.EstrategiaDetalle)
                .Include(o => o.Actividades)
                .Include(o => o.Materiales)
                .Include(o => o.Personal).ThenInclude(p => p.Empleado).ThenInclude(e => e.Rol)
                .Include(o => o.Sistema)
                .Include(o => o.SubSistema)
                .FirstOrDefaultAsync(o => o.id_ot == idOt);
        }

        public async Task<OrdenTrabajo> CreateAsync(OrdenTrabajo ot)
        {
            await _context.OrdenesTrabajoMant.AddAsync(ot);
            await _context.SaveChangesAsync();
            return ot;
        }

        public async Task UpdateAsync(OrdenTrabajo ot)
        {
            _context.OrdenesTrabajoMant.Update(ot);
            await _context.SaveChangesAsync();
        }

        // ── PMUltimaIntervencion ──────────────────────────────────

        public async Task<PMUltimaIntervencion> GetUltimaIntervencionAsync(int idEquipo, int idDetalleEstrg)
        {
            return await _context.PMUltimasIntervenciones
                .FirstOrDefaultAsync(p => p.id_equipo == idEquipo && p.id_detalle_estrg == idDetalleEstrg);
        }

        public async Task UpsertUltimaIntervencionAsync(PMUltimaIntervencion intervencion)
        {
            var existing = await _context.PMUltimasIntervenciones
                .FindAsync(intervencion.id_equipo, intervencion.id_detalle_estrg);

            if (existing == null)
                await _context.PMUltimasIntervenciones.AddAsync(intervencion);
            else
            {
                existing.horometro_corte = intervencion.horometro_corte;
                existing.fecha_corte     = intervencion.fecha_corte;
                existing.id_ot           = intervencion.id_ot;
            }

            await _context.SaveChangesAsync();
        }

        // ── Calendario ────────────────────────────────────────────

        public async Task<IEnumerable<CalendarioMantenimiento>> GetCalendarioByEquipoAsync(int idEquipo)
        {
            return await _context.CalendariosMantenimiento
                .Include(c => c.EstrategiaDetalle)
                .Include(c => c.OrdenTrabajo)
                .Where(c => c.id_equipo == idEquipo && !c.ejecutado)
                .OrderBy(c => c.fecha_estimada)
                .ToListAsync();
        }

        public async Task UpsertCalendarioAsync(CalendarioMantenimiento calendario)
        {
            var existing = await _context.CalendariosMantenimiento
                .FirstOrDefaultAsync(c =>
                    c.id_equipo == calendario.id_equipo &&
                    c.id_detalle_estrg == calendario.id_detalle_estrg &&
                    !c.ejecutado);

            if (existing == null)
                await _context.CalendariosMantenimiento.AddAsync(calendario);
            else
            {
                existing.horometro_base      = calendario.horometro_base;
                existing.fecha_base          = calendario.fecha_base;
                existing.horas_diarias_usadas = calendario.horas_diarias_usadas;
                existing.fecha_estimada      = calendario.fecha_estimada;
            }

            await _context.SaveChangesAsync();
        }

        // ── Configuración Flota ───────────────────────────────────

        public async Task<ConfiguracionFlota> GetConfiguracionFlotaAsync(int? idFlota)
        {
            // Busca config específica de flota; si no existe, busca la global (id_flota == null)
            return await _context.ConfiguracionesFlota
                .FirstOrDefaultAsync(c => c.id_flota == idFlota)
                ?? await _context.ConfiguracionesFlota
                    .FirstOrDefaultAsync(c => c.id_flota == null);
        }

        public async Task UpsertConfiguracionFlotaAsync(ConfiguracionFlota config)
        {
            var existing = await _context.ConfiguracionesFlota
                .FirstOrDefaultAsync(c => c.id_flota == config.id_flota);

            if (existing == null)
                await _context.ConfiguracionesFlota.AddAsync(config);
            else
            {
                existing.horas_diarias_estimadas = config.horas_diarias_estimadas;
                existing.fecha_actualizacion     = config.fecha_actualizacion;
                existing.actualizado_por         = config.actualizado_por;
            }

            await _context.SaveChangesAsync();
        }

        // ── Validación duplicados ─────────────────────────────────

        public async Task<bool> ExisteOTActivaAsync(int idEquipo, int idDetalleEstrg)
        {
            return await _context.OrdenesTrabajoMant
                .AnyAsync(o =>
                    o.id_equipo == idEquipo &&
                    (o.estado == "PENDIENTE" || o.estado == "ACTIVA" || o.estado == "EN_REVISION") &&
                    o.PlanesDetalle.Any(d => d.id_detalle_estrg == idDetalleEstrg));
        }
    }
}
