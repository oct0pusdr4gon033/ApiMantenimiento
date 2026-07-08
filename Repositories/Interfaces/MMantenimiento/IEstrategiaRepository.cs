using System.Collections.Generic;
using System.Threading.Tasks;
using ApiMantenimiento.Models.Entitys.MMantenimiento;

namespace ApiMantenimiento.Repositories.Interfaces.MMantenimiento
{
    public interface IEstrategiaRepository
    {
        Task<IEnumerable<Estrategia>> GetAllEstrategiasAsync();
        Task<Estrategia> GetEstrategiaByIdAsync(int id);
        Task<Estrategia> AddEstrategiaAsync(Estrategia estrategia);
        Task<Estrategia> UpdateEstrategiaAsync(Estrategia estrategia);
        Task<bool> EstrategiaExistsAsync(int id);
    }
}
