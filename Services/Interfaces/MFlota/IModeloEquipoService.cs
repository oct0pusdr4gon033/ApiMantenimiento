using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IModeloEquipoService
    {
        Task<ApiResponse<IEnumerable<ModeloEquipoResponse>>> ListarAsync();
        Task<ApiResponse<ModeloEquipoResponse>> BuscarPorIdAsync(int id);
        Task<ApiResponse<string>> AgregarAsync(ModeloEquipoRequest request);
        Task<ApiResponse<string>> ActualizarAsync(int id, ModeloEquipoRequest request);
    }
}
