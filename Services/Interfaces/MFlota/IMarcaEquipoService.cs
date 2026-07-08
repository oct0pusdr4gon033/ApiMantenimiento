using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IMarcaEquipoService
    {
        Task<ApiResponse<IEnumerable<MarcaEquipoResponse>>> ListarAsync();
        Task<ApiResponse<string>> AgregarAsync(MarcaEquipoRequest request);
        Task<ApiResponse<string>> ActualizarAsync(int id, MarcaEquipoRequest request);
    }
}
