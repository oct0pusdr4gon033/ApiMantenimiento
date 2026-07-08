using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Models.Responses.MEmpleados;

namespace ApiMantenimiento.Services.Interfaces.MEmpleados
{
    public interface IEmpleadoService
    {
        Task<ApiResponse<IEnumerable<EmpleadoResponse>>> ObtenerTodosActivosAsync();
        Task<ApiResponse<IEnumerable<EmpleadoResponse>>> ObtenerTodosAsync();
        Task<ApiResponse<EmpleadoResponse>> ObtenerPorDniAsync(string dni);
        Task<ApiResponse<EmpleadoResponse>> CrearAsync(EmpleadoRequest request);
        Task<ApiResponse<EmpleadoResponse>> ActualizarAsync(string dni, EmpleadoRequest request);
        Task<ApiResponse<bool>> EliminarLogicoAsync(string dni);
    }
}
