using ApiMantenimiento.Models.Entitys.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MCompras
{
    public interface ISolicitudPedidoRepository
    {
        Task<IEnumerable<SolicitudPedido>> GetAllAsync();
        Task<SolicitudPedido> GetByIdAsync(int id);
        Task<SolicitudPedido> AddAsync(SolicitudPedido solicitud);
        Task UpdateAsync(SolicitudPedido solicitud);
    }
}
