using ApiMantenimiento.Models.Entitys.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MAlmacen
{
    public interface IUnidadMedidaRepository
    {
        Task<IEnumerable<UnidadMedida>> GetAllAsync();
        Task<UnidadMedida> GetByIdAsync(int id);
        Task<UnidadMedida> AddAsync(UnidadMedida unidad);
        Task UpdateAsync(UnidadMedida unidad);
    }
}
