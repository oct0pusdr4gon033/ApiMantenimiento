using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MAlmacen
{
    public class UnidadMedidaRepository : IUnidadMedidaRepository
    {
        private readonly MantenimientoDbContext _context;

        public UnidadMedidaRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UnidadMedida>> GetAllAsync()
        {
            return await _context.UnidadesMedida.ToListAsync();
        }

        public async Task<UnidadMedida> GetByIdAsync(int id)
        {
            return await _context.UnidadesMedida.FindAsync(id);
        }

        public async Task<UnidadMedida> AddAsync(UnidadMedida unidad)
        {
            await _context.UnidadesMedida.AddAsync(unidad);
            await _context.SaveChangesAsync();
            return unidad;
        }

        public async Task UpdateAsync(UnidadMedida unidad)
        {
            _context.UnidadesMedida.Update(unidad);
            await _context.SaveChangesAsync();
        }
    }
}
