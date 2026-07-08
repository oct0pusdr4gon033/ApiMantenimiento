using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MMantenimiento
{
    public class SistemaEquipoRepository : ISistemaEquipoRepository
    {
        private readonly MantenimientoDbContext _context;

        public SistemaEquipoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SistemaEquipo>> GetAllAsync()
        {
            return await _context.SistemasEquipos.ToListAsync();
        }

        public async Task<SistemaEquipo> GetByIdAsync(int id)
        {
            return await _context.SistemasEquipos.FindAsync(id);
        }

        public async Task<SistemaEquipo> AddAsync(SistemaEquipo sistema)
        {
            await _context.SistemasEquipos.AddAsync(sistema);
            await _context.SaveChangesAsync();
            return sistema;
        }

        public async Task UpdateAsync(SistemaEquipo sistema)
        {
            _context.SistemasEquipos.Update(sistema);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubSistemaEquipo>> GetSubSistemasBySistemaAsync(int idSistema)
        {
            return await _context.SubSistemasEquipos
                .Where(s => s.id_sistema == idSistema)
                .ToListAsync();
        }

        public async Task<SubSistemaEquipo> AddSubSistemaAsync(SubSistemaEquipo subSistema)
        {
            await _context.SubSistemasEquipos.AddAsync(subSistema);
            await _context.SaveChangesAsync();
            return subSistema;
        }
    }
}
