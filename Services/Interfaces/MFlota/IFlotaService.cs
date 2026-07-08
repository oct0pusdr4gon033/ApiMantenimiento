using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Responses;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IFlotaService
    {
        // ── Listado general ──────────────────────────────────
        Task<ApiResponse<IEnumerable<FlotaResponse>>> ListarAsync();

        // ── Búsquedas ────────────────────────────────────────
        Task<ApiResponse<FlotaResponse>> BuscarPorIdAsync(int id);
        Task<ApiResponse<FlotaResponse>> BuscarPorCodigoAsync(string codFlota);

        /// <summary>Busca flotas cuyo tipo de equipo contenga el texto (ej: "CAMION", "VOLQUETE").</summary>
        Task<ApiResponse<IEnumerable<FlotaResponse>>> BuscarPorTipoEquipoAsync(string nombreTipo);

        /// <summary>Busca flotas cuyo modelo contenga el texto (búsqueda parcial).</summary>
        Task<ApiResponse<IEnumerable<FlotaResponse>>> BuscarPorModeloAsync(string nombreModelo);

        /// <summary>
        /// Retorna el detalle completo de una flota: sus datos + todos los equipos asociados
        /// con su jerarquía (Marca, Modelo, TipoEquipo, AreaOperacion).
        /// </summary>
        Task<ApiResponse<FlotaDetalleResponse>> ObtenerDetalleAsync(string codFlota);

        // ── Mutaciones ───────────────────────────────────────
        Task<ApiResponse<string>> AgregarAsync(FlotaRequest request);
        Task<ApiResponse<string>> ActualizarAsync(int id, FlotaRequest request);
    }
}
