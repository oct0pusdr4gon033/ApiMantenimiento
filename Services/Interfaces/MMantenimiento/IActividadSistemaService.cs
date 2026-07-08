using ApiMantenimiento.Models.DTOS.MMantenimiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MMantenimiento
{
    public interface IActividadSistemaService
    {
        Task<IEnumerable<ActividadSistemaResponse>> GetAllAsync();
        Task<IEnumerable<ActividadSistemaResponse>> BuscarPorSistemaONombreAsync(string termino);
        Task<ActividadSistemaResponse> CreateAsync(ActividadSistemaRequest request);
        Task<ActividadSistemaResponse> UpdateAsync(int id, ActividadSistemaUpdateRequest request);
    }
}
