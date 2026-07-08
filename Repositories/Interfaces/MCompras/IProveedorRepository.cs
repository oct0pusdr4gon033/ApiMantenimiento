using ApiMantenimiento.Models.Entitys.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.Interfaces.MCompras
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> GetAllAsync();
        Task<Proveedor> GetByRucAsync(string ruc);
        Task<IEnumerable<Proveedor>> BuscarProveedoresAsync(string? ruc, string? razonSocial, string? codCat);
        Task<Proveedor> AddAsync(Proveedor proveedor);
        Task UpdateAsync(Proveedor proveedor);
        Task<bool> ExistsByRucAsync(string ruc);
        Task<IEnumerable<CategoriaProveedor>> GetCategoriasAsync();
        Task<CategoriaProveedor> GetCategoriaByCodAsync(string codCat);
        Task<CategoriaProveedor> AddCategoriaAsync(CategoriaProveedor categoria);
    }
}
