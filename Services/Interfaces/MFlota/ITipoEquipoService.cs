using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface ITipoEquipoService
    {
        Task<ApiResponse<IEnumerable<TipoEquipoResponse>>> ListarAsync();
        Task<ApiResponse<TipoEquipoResponse>> BuscarPorIdAsync(int id);
        Task<ApiResponse<IEnumerable<TipoEquipoResponse>>> BuscarPorFiltroAsync(string filtro);
        Task<ApiResponse<string>> AgregarAsync(TipoEquipoRequest request);
        Task<ApiResponse<string>> ActualizarAsync(int id, TipoEquipoRequest request);
    }
}
