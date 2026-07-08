using ApiMantenimiento.Models.Entitys.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MCompras
{
    public interface INotaIngresoRepository
    {
        Task<IEnumerable<NotaIngreso>> GetAllAsync();
        Task<NotaIngreso> GetByIdAsync(int id);
        Task<NotaIngreso> AddAsync(NotaIngreso nota);
        Task UpdateAsync(NotaIngreso nota);
    }
}
