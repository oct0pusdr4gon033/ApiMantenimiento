using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MMantenimiento
{
    public class ActividadSistemaRepository : IActividadSistemaRepository
    {
        private readonly MantenimientoDbContext _context;

        public ActividadSistemaRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActividadSistema>> GetAllAsync()
        {
            return await _context.ActividadesSistemas
                .Include(a => a.SistemaEquipo)
                .ToListAsync();
        }

        public async Task<ActividadSistema> GetByIdAsync(int id)
        {
            return await _context.ActividadesSistemas
                .Include(a => a.SistemaEquipo)
                .FirstOrDefaultAsync(a => a.id_actividad == id);
        }

        public async Task<IEnumerable<ActividadSistema>> BuscarPorSistemaONombreAsync(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return await GetAllAsync();

            var query = termino.ToLower();
            return await _context.ActividadesSistemas
                .Include(a => a.SistemaEquipo)
                .Where(a => a.SistemaEquipo.cod_sist.ToLower().Contains(query) ||
                            a.nombre_actividad.ToLower().Contains(query))
                .ToListAsync();
        }

        public async Task<string> GetUltimoCodigoActividadAsync(int id_sistema)
        {
            var ultimaActividad = await _context.ActividadesSistemas
                .Where(a => a.id_sistema == id_sistema)
                .OrderByDescending(a => a.cod_act)
                .FirstOrDefaultAsync();

            return ultimaActividad?.cod_act;
        }

        public async Task<ActividadSistema> AddAsync(ActividadSistema actividad)
        {
            await _context.ActividadesSistemas.AddAsync(actividad);
            await _context.SaveChangesAsync();
            return actividad;
        }

        public async Task UpdateAsync(ActividadSistema actividad)
        {
            _context.ActividadesSistemas.Update(actividad);
            await _context.SaveChangesAsync();
        }
    }
}
