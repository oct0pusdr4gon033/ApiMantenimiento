using ApiMantenimiento.Models.Entitys.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MCompras
{
    public interface ICotizacionRepository
    {
        Task<IEnumerable<Cotizacion>> GetAllAsync();
        Task<Cotizacion> GetByIdAsync(int id);
        Task<Cotizacion> AddAsync(Cotizacion cotizacion);
        Task UpdateAsync(Cotizacion cotizacion);
    }
}
