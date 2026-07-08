using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IExpedienteService
    {
        Task<ApiResponse<IEnumerable<ExpedienteResponse>>> ListarAsync();
        Task<ApiResponse<ExpedienteResponse>> BuscarPorCodigoAsync(string codigo);
        Task<ApiResponse<ExpedienteResponse>> BuscarPorEquipoAsync(int idEquipo);
        Task<ApiResponse<string>> CrearExpedienteAsync(ExpedienteRequest request);
        Task<ApiResponse<string>> InsertarDocumentoAsync(ExpedienteDocumentoRequest request);
        Task<ApiResponse<string>> ActualizarDocumentoAsync(int idDocumento, ExpedienteDocumentoRequest request);
        Task<ApiResponse<ExpedienteDocumentoResponse>> ObtenerDocumentoPorIdAsync(int id);
    }
}
