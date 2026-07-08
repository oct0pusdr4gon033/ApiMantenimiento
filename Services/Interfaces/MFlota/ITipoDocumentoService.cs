using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface ITipoDocumentoService
    {
        Task<ApiResponse<IEnumerable<TipoDocumentoResponse>>> ListarAsync();
        Task<ApiResponse<TipoDocumentoResponse>> BuscarPorCodigoAsync(string codigo);
        Task<ApiResponse<string>> AgregarAsync(TipoDocumentoRequest request);
        Task<ApiResponse<string>> ActualizarAsync(string codigo, TipoDocumentoRequest request);
    }
}
