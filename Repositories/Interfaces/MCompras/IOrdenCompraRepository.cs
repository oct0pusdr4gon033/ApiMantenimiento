using ApiMantenimiento.Models.Entitys.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MCompras
{
    public interface IOrdenCompraRepository
    {
        Task<IEnumerable<OrdenCompra>> GetAllAsync();
        Task<OrdenCompra> GetByIdAsync(int id);
        Task<OrdenCompra> AddAsync(OrdenCompra orden);
        Task UpdateAsync(OrdenCompra orden);
    }
}
