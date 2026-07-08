using System.Collections.Generic;
using System.Threading.Tasks;
using ApiMantenimiento.Models.DTOS.MMantenimiento;

namespace ApiMantenimiento.Services.Interfaces.MMantenimiento
{
    public interface IEstrategiaService
    {
        Task<IEnumerable<EstrategiaResponse>> GetAllEstrategiasAsync();
        Task<EstrategiaResponse> GetEstrategiaByIdAsync(int id);
        Task<EstrategiaResponse> CreateEstrategiaAsync(EstrategiaRequest request);
        Task<EstrategiaResponse> UpdateEstrategiaAsync(int id, EstrategiaUpdateRequest request);
    }
}
