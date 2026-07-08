using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IEquipoService
    {
        Task<ApiResponse<IEnumerable<EquipoResponse>>> ListarAsync();
        Task<ApiResponse<EquipoResponse>> BuscarPorIdAsync(int id);
        Task<ApiResponse<EquipoResponse>> BuscarPorCodigoAsync(string codigo);
        Task<ApiResponse<EquipoResponse>> BuscarPorPlacaAsync(string placa);
        Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorAreaOperacionAsync(string codArea);
        Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorCodigoFlotaAsync(string codFlota);
        Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorTipoEquipoAsync(int idTipoEqp);
        Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorMarcaAsync(int idMarca);
        Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorModeloAsync(int idModelo);
        Task<ApiResponse<string>> AgregarAsync(EquipoRequest request);
        Task<ApiResponse<string>> ActualizarAsync(int id, EquipoRequest request);
    }
}
