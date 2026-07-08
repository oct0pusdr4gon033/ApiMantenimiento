using ApiMantenimiento.Models.Requests.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Models.Responses.MFlota;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IHistorialHorometroService
    {
        Task<ApiResponse<IEnumerable<HistorialHorometroResponse>>> ObtenerTodosAsync();
        Task<ApiResponse<IEnumerable<HistorialHorometroResponse>>> ObtenerPorEquipoAsync(int idEquipo);
        Task<ApiResponse<HistorialHorometroResponse>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<HistorialHorometroResponse>> CrearAsync(HistorialHorometroRequest request);
    }
}
