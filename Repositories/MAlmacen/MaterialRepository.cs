using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MAlmacen
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly MantenimientoDbContext _context;

        public MaterialRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _context.Materiales
                .Include(m => m.UnidadMedida)
                .Include(m => m.CategoriaMaterial)
                .ToListAsync();
        }

        public async Task<Material> GetByIdAsync(int id)
        {
            return await _context.Materiales
                .Include(m => m.UnidadMedida)
                .Include(m => m.CategoriaMaterial)
                .FirstOrDefaultAsync(m => m.id_material == id);
        }

        public async Task<IEnumerable<Material>> BuscarMaterialesAsync(string cod_materia, string nombre, string estado, int? id_unidad, int? id_categoria)
        {
            var query = _context.Materiales
                .Include(m => m.UnidadMedida)
                .Include(m => m.CategoriaMaterial)
                .AsQueryable();

            if (!string.IsNullOrEmpty(cod_materia))
            {
                query = query.Where(m => m.cod_materia.Contains(cod_materia));
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(m => m.descripcion.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(m => m.estado == estado);
            }

            if (id_unidad.HasValue && id_unidad.Value > 0)
            {
                query = query.Where(m => m.id_unidad == id_unidad.Value);
            }

            if (id_categoria.HasValue && id_categoria.Value > 0)
            {
                query = query.Where(m => m.id_categoria == id_categoria.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Material> AddAsync(Material material)
        {
            await _context.Materiales.AddAsync(material);
            await _context.SaveChangesAsync();
            return material;
        }

        public async Task UpdateAsync(Material material)
        {
            _context.Materiales.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByCodMateriaAsync(string cod_materia)
        {
            return await _context.Materiales.AnyAsync(m => m.cod_materia == cod_materia);
        }
    }
}
