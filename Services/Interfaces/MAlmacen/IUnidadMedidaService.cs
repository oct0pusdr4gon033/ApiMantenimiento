using ApiMantenimiento.Models.DTOS.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MAlmacen
{
    public interface IUnidadMedidaService
    {
        Task<IEnumerable<UnidadMedidaResponse>> GetAllAsync();
        Task<UnidadMedidaResponse> CreateAsync(UnidadMedidaRequest request);
        Task<UnidadMedidaResponse> UpdateAsync(int id, UnidadMedidaRequest request);
    }
}
