using ApiMantenimiento.Models.DTOS.MMantenimiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MMantenimiento
{
    public interface ISistemaEquipoService
    {
        Task<IEnumerable<SistemaEquipoResponse>> GetAllAsync();
        Task<SistemaEquipoResponse> CreateAsync(SistemaEquipoRequest request);
        Task<SistemaEquipoResponse> UpdateAsync(int id, SistemaEquipoUpdateRequest request);
        Task<IEnumerable<SubSistemaResponse>> GetSubSistemasBySistemaAsync(int idSistema);
        Task<SubSistemaResponse> CreateSubSistemaAsync(SubSistemaRequest request);
    }
}
