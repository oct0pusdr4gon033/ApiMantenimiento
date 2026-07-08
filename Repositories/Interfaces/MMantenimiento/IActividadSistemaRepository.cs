using ApiMantenimiento.Models.Entitys.MMantenimiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MMantenimiento
{
    public interface IActividadSistemaRepository
    {
        Task<IEnumerable<ActividadSistema>> GetAllAsync();
        Task<ActividadSistema> GetByIdAsync(int id);
        Task<IEnumerable<ActividadSistema>> BuscarPorSistemaONombreAsync(string termino);
        Task<string> GetUltimoCodigoActividadAsync(int id_sistema);
        Task<ActividadSistema> AddAsync(ActividadSistema actividad);
        Task UpdateAsync(ActividadSistema actividad);
    }
}
