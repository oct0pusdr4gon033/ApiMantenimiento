using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MAlmacen
{
    public interface IMovimientoInventarioRepository
    {
        Task<IEnumerable<MovimientoInventarioResponse>> GetByMaterialIdAsync(int idMaterial);
        Task<MovimientoInventario> AddAsync(MovimientoInventario movimiento);
    }
}
