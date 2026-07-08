using ApiMantenimiento.Models.Entitys.MMantenimiento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MMantenimiento
{
    public interface ISistemaEquipoRepository
    {
        Task<IEnumerable<SistemaEquipo>> GetAllAsync();
        Task<SistemaEquipo> GetByIdAsync(int id);
        Task<SistemaEquipo> AddAsync(SistemaEquipo sistema);
        Task UpdateAsync(SistemaEquipo sistema);
        Task<IEnumerable<SubSistemaEquipo>> GetSubSistemasBySistemaAsync(int idSistema);
        Task<SubSistemaEquipo> AddSubSistemaAsync(SubSistemaEquipo subSistema);
    }
}
