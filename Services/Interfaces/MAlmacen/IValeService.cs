using ApiMantenimiento.Models.DTOS.MAlmacen;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MAlmacen
{
    public interface IValeService
    {
        Task<ValeResponse> GetByIdAsync(int id);
        Task<ValeResponse?> GetByOtIdAsync(int idOt);
        Task<IEnumerable<ValeResponse>> GetAllAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, string? search);
        Task<ValeResponse> CreateAsync(ValeCreateRequest request);
        Task<ValeResponse> UpdateAsync(int id, ValeUpdateRequest request);
        Task DeleteAsync(int id);
        Task<ValeResponse> DispatchAsync(int id, ValeDispatchRequest request);
        Task<IEnumerable<ReservedMaterialResponse>> GetReservedMaterialsAsync();
    }
}
