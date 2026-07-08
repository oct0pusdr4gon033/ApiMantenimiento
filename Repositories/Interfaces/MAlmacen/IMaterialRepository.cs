using ApiMantenimiento.Models.Entitys.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MAlmacen
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAllAsync();
        Task<Material> GetByIdAsync(int id);
        
        // Advanced search requested by the user
        Task<IEnumerable<Material>> BuscarMaterialesAsync(string cod_materia, string nombre, string estado, int? id_unidad, int? id_categoria);
        
        Task<Material> AddAsync(Material material);
        Task UpdateAsync(Material material);
        Task<bool> ExistsByCodMateriaAsync(string cod_materia);
    }
}
