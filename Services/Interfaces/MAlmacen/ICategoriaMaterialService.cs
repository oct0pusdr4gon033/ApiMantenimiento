using ApiMantenimiento.Models.DTOS.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MAlmacen
{
    public interface ICategoriaMaterialService
    {
        Task<IEnumerable<CategoriaMaterialResponse>> GetAllAsync();
        Task<CategoriaMaterialResponse> CreateAsync(CategoriaMaterialRequest request);
        Task<CategoriaMaterialResponse> UpdateAsync(int id, CategoriaMaterialRequest request);
    }
}
