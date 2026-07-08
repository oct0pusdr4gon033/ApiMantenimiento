using ApiMantenimiento.Models.Entitys.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MAlmacen
{
    public interface ICategoriaMaterialRepository
    {
        Task<IEnumerable<CategoriaMaterial>> GetAllAsync();
        Task<CategoriaMaterial> GetByIdAsync(int id);
        Task<CategoriaMaterial> AddAsync(CategoriaMaterial categoria);
        Task UpdateAsync(CategoriaMaterial categoria);
        Task<bool> ExistsByCodCatAsync(string cod_cat);
    }
}
