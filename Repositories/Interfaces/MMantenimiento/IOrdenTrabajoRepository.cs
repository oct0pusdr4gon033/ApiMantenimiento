using ApiMantenimiento.Models.Entitys.MMantenimiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MMantenimiento
{
    public interface IOrdenTrabajoRepository
    {
        Task<IEnumerable<OrdenTrabajo>> GetAllAsync();
        Task<IEnumerable<OrdenTrabajo>> GetByEquipoAsync(int idEquipo);
        Task<OrdenTrabajo> GetByIdAsync(int idOt);
        Task<OrdenTrabajo> CreateAsync(OrdenTrabajo ot);
        Task UpdateAsync(OrdenTrabajo ot);

        // PMUltimaIntervencion
        Task<PMUltimaIntervencion> GetUltimaIntervencionAsync(int idEquipo, int idDetalleEstrg);
        Task UpsertUltimaIntervencionAsync(PMUltimaIntervencion intervencion);

        // Calendario
        Task<IEnumerable<CalendarioMantenimiento>> GetCalendarioByEquipoAsync(int idEquipo);
        Task UpsertCalendarioAsync(CalendarioMantenimiento calendario);

        // Configuración de flota
        Task<ConfiguracionFlota> GetConfiguracionFlotaAsync(int? idFlota);
        Task UpsertConfiguracionFlotaAsync(ConfiguracionFlota config);

        // Verificar OT activa/pendiente para un equipo+PM
        Task<bool> ExisteOTActivaAsync(int idEquipo, int idDetalleEstrg);
    }
}
