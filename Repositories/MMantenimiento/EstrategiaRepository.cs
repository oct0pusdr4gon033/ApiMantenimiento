using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;

namespace ApiMantenimiento.Repositories.MMantenimiento
{
    public class EstrategiaRepository : IEstrategiaRepository
    {
        private readonly MantenimientoDbContext _context;

        public EstrategiaRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estrategia>> GetAllEstrategiasAsync()
        {
            return await _context.Estrategias
                .Include(e => e.Flota)
                .Include(e => e.Equipo)
                .Include(e => e.Detalles)
                .ToListAsync();
        }

        public async Task<Estrategia> GetEstrategiaByIdAsync(int id)
        {
            return await _context.Estrategias
                .Include(e => e.Flota)
                .Include(e => e.Equipo)
                .Include(e => e.Detalles)
                .FirstOrDefaultAsync(e => e.id_estrategia == id);
        }

        public async Task<Estrategia> AddEstrategiaAsync(Estrategia estrategia)
        {
            _context.Estrategias.Add(estrategia);
            await _context.SaveChangesAsync();
            return estrategia;
        }

        public async Task<Estrategia> UpdateEstrategiaAsync(Estrategia estrategia)
        {
            _context.Entry(estrategia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return estrategia;
        }

        public async Task<bool> EstrategiaExistsAsync(int id)
        {
            return await _context.Estrategias.AnyAsync(e => e.id_estrategia == id);
        }
    }
}
