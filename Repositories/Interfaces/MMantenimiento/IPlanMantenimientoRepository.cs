using ApiMantenimiento.Models.Entitys.MMantenimiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MMantenimiento
{
    public interface IPlanMantenimientoRepository
    {
        Task<IEnumerable<PlanMantenimiento>> GetAllAsync();
        Task<PlanMantenimiento> GetByIdAsync(int id);
        Task<PlanMantenimiento> CreateAsync(PlanMantenimiento planMantenimiento);
        Task UpdateAsync(PlanMantenimiento planMantenimiento);
        Task DeleteAsync(int id);
        
        Task AddActividadAsync(PlanMantenimientoActividad actividad);
        Task RemoveActividadAsync(int id_plan_mant, int id_actividad, int id_detalle_estrg);

        Task AddPersonalAsync(PlanMantenimientoPersonal personal);
        Task RemovePersonalAsync(int id_plan_personal);
    }
}
