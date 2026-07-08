using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MMantenimiento
{
    public interface IPlanMantenimientoService
    {
        Task<ApiResponse<IEnumerable<PlanMantenimientoResponse>>> GetAllAsync();
        Task<ApiResponse<PlanMantenimientoResponse>> GetByIdAsync(int id);
        Task<ApiResponse<PlanMantenimientoResponse>> CreateAsync(PlanMantenimientoRequest request);
        Task<ApiResponse<PlanMantenimientoResponse>> UpdateAsync(int id, PlanMantenimientoUpdateRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);

        Task<ApiResponse<PlanMantenimientoActividadResponse>> AddActividadAsync(int id_plan_mant, PlanMantenimientoActividadRequest request);
        Task<ApiResponse<bool>> RemoveActividadAsync(int id_plan_mant, int id_actividad, int id_detalle_estrg);

        Task<ApiResponse<PlanMantenimientoPersonalResponse>> AddPersonalAsync(int id_plan_mant, PlanMantenimientoPersonalRequest request);
        Task<ApiResponse<bool>> RemovePersonalAsync(int id_plan_personal);
    }
}
