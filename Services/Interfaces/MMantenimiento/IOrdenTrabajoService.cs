using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MMantenimiento
{
    public interface IOrdenTrabajoService
    {
        Task<ApiResponse<IEnumerable<OrdenTrabajoResponse>>> GetAllAsync();
        Task<ApiResponse<IEnumerable<OrdenTrabajoResponse>>> GetByEquipoAsync(int idEquipo);
        Task<ApiResponse<OrdenTrabajoResponse>> GetByIdAsync(int idOt);

        /// <summary>Crear OT manual (PREVENTIVA o CORRECTIVA).</summary>
        Task<ApiResponse<OrdenTrabajoResponse>> CreateManualAsync(OrdenTrabajoCreateRequest request);

        /// <summary>Cambiar estado de la OT. Al CERRAR registra horometro_corte y fecha_atencion.</summary>
        Task<ApiResponse<OrdenTrabajoResponse>> CambiarEstadoAsync(int idOt, CambiarEstadoOTRequest request);

        /// <summary>
        /// Evalúa si el horómetro actual de un equipo activa algún umbral de PM.
        /// Genera OT automáticas si aplica. Llamado desde HistorialHorometroService.
        /// </summary>
        Task EvaluarUmbralesYGenerarOTsAsync(int idEquipo, decimal horometroActual);

        /// <summary>Recalcular proyección de calendario para un equipo.</summary>
        Task RecalcularCalendarioAsync(int idEquipo);

        /// <summary>Obtener proyección de calendario para un equipo.</summary>
        Task<ApiResponse<CalendarioProyeccionResponse>> GetCalendarioAsync(int idEquipo);

        /// <summary>Obtener proyección de calendario para toda una flota.</summary>
        Task<ApiResponse<IEnumerable<CalendarioProyeccionResponse>>> GetCalendarioFlotaAsync(int idFlota);

        /// <summary>Configurar horas estimadas de operación por flota.</summary>
        Task<ApiResponse<ConfiguracionFlotaResponse>> SetConfiguracionFlotaAsync(ConfiguracionFlotaRequest request);
        Task<ApiResponse<ConfiguracionFlotaResponse>> GetConfiguracionFlotaAsync(int? idFlota);

        // Actividades y Materiales Extra
        Task<ApiResponse<OTActividadResponse>> AddActividadExtraAsync(int idOt, AgregarActividadOTRequest request);
        Task<ApiResponse<bool>> RemoveActividadExtraAsync(int idOtActividad);
        Task<ApiResponse<OTMaterialResponse>> AddMaterialExtraAsync(int idOt, AgregarMaterialOTRequest request);
        Task<ApiResponse<bool>> RemoveMaterialExtraAsync(int idOtMaterial);

        // Personal Extra
        Task<ApiResponse<OTPersonalResponse>> AddPersonalExtraAsync(int idOt, AgregarPersonalOTRequest request);
        Task<ApiResponse<bool>> RemovePersonalExtraAsync(int idOtPersonal);
        Task<ApiResponse<IEnumerable<EmpleadoDisponibleResponse>>> GetTecnicosDisponiblesAsync(int idOt);
    }
}
