using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MAlmacen
{
    public class CategoriaMaterialRepository : ICategoriaMaterialRepository
    {
        private readonly MantenimientoDbContext _context;

        public CategoriaMaterialRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaMaterial>> GetAllAsync()
        {
            return await _context.CategoriasMaterial.ToListAsync();
        }

        public async Task<CategoriaMaterial> GetByIdAsync(int id)
        {
            return await _context.CategoriasMaterial.FindAsync(id);
        }

        public async Task<CategoriaMaterial> AddAsync(CategoriaMaterial categoria)
        {
            await _context.CategoriasMaterial.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task UpdateAsync(CategoriaMaterial categoria)
        {
            _context.CategoriasMaterial.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByCodCatAsync(string cod_cat)
        {
            return await _context.CategoriasMaterial.AnyAsync(c => c.cod_cat == cod_cat);
        }
    }
}
