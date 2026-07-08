using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Models.Responses.MEmpleados;

namespace ApiMantenimiento.Services.Interfaces.MEmpleados
{
    public interface IRolService
    {
        Task<ApiResponse<IEnumerable<RolResponse>>> ObtenerTodosAsync();
        Task<ApiResponse<RolResponse>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<RolResponse>> CrearAsync(RolRequest request);
        Task<ApiResponse<RolResponse>> ActualizarAsync(int id, RolRequest request);
    }
}
