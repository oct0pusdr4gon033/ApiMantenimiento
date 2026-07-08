using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MAlmacen
{
    public interface IValeRepository
    {
        Task<Vale?> GetByIdAsync(int id);
        Task<Vale?> GetByOtIdAsync(int idOt);
        Task<IEnumerable<Vale>> GetAllAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, string? search);
        Task<Vale> AddAsync(Vale vale);
        Task UpdateAsync(Vale vale);
        Task DeleteAsync(Vale vale);
        Task<IEnumerable<ReservedMaterialResponse>> GetReservedMaterialsAsync();
    }
}
