using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MCompras
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly MantenimientoDbContext _context;

        public ProveedorRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync()
        {
            return await _context.Proveedores
                .Include(p => p.Contactos)
                .Include(p => p.ProveedorCategorias)
                    .ThenInclude(pc => pc.CategoriaProveedor)
                .ToListAsync();
        }

        public async Task<Proveedor> GetByRucAsync(string ruc)
        {
            return await _context.Proveedores
                .Include(p => p.Contactos)
                .Include(p => p.ProveedorCategorias)
                    .ThenInclude(pc => pc.CategoriaProveedor)
                .FirstOrDefaultAsync(p => p.ruc == ruc);
        }

        public async Task<IEnumerable<Proveedor>> BuscarProveedoresAsync(string? ruc, string? razonSocial, string? codCat)
        {
            var query = _context.Proveedores
                .Include(p => p.Contactos)
                .Include(p => p.ProveedorCategorias)
                    .ThenInclude(pc => pc.CategoriaProveedor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ruc))
            {
                query = query.Where(p => p.ruc.Contains(ruc));
            }

            if (!string.IsNullOrEmpty(razonSocial))
            {
                query = query.Where(p => p.razon_social.Contains(razonSocial) || p.nombre_comercial.Contains(razonSocial));
            }

            if (!string.IsNullOrEmpty(codCat))
            {
                query = query.Where(p => p.ProveedorCategorias.Any(pc => pc.cod_cat == codCat));
            }

            return await query.ToListAsync();
        }

        public async Task<Proveedor> AddAsync(Proveedor proveedor)
        {
            await _context.Proveedores.AddAsync(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task UpdateAsync(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByRucAsync(string ruc)
        {
            return await _context.Proveedores.AnyAsync(p => p.ruc == ruc);
        }

        public async Task<IEnumerable<CategoriaProveedor>> GetCategoriasAsync()
        {
            return await _context.CategoriaProveedores.ToListAsync();
        }

        public async Task<CategoriaProveedor> GetCategoriaByCodAsync(string codCat)
        {
            return await _context.CategoriaProveedores.FirstOrDefaultAsync(c => c.cod_cat == codCat);
        }

        public async Task<CategoriaProveedor> AddCategoriaAsync(CategoriaProveedor categoria)
        {
            await _context.CategoriaProveedores.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}
