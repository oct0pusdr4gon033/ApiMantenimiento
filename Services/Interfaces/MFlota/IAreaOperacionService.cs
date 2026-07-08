using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IAreaOperacionService
    {
        Task<ApiResponse<IEnumerable<AreaOperacionResponse>>> ListarAreasAsync();
        Task<ApiResponse<AreaOperacionResponse>> BuscarPorCodigoAsync(string codigoArea);
        Task<ApiResponse<AreaOperacionResponse>> BuscarPorNombreAsync(string nombreArea);
        
        // CORRECCIÓN: Usamos el nombre y el Request que tienes en tu clase
        Task<ApiResponse<string>> AgregarAreaAsync(AreaOperacionRequest request);
        Task<ApiResponse<string>> ActualizarAreaAsync(string codigoArea, AreaOperacionRequest request);
    }
}
