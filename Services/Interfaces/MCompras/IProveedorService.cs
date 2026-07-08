using ApiMantenimiento.Models.DTOS.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MCompras
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorResponse>> GetAllAsync();
        Task<ProveedorResponse> GetByRucAsync(string ruc);
        Task<IEnumerable<ProveedorResponse>> BuscarProveedoresAsync(string? ruc, string? razonSocial, string? codCat);
        Task<ProveedorResponse> CreateAsync(ProveedorRequest request);
        Task<ProveedorResponse> UpdateAsync(string ruc, ProveedorRequest request);
        Task<ProveedorContactoResponse> AddContactoAsync(string ruc, ProveedorContactoRequest request);
        Task<ProveedorContactoResponse> UpdateContactoAsync(int idContacto, ProveedorContactoRequest request);
        Task<IEnumerable<CategoriaProveedorResponse>> GetCategoriasAsync();
        Task<CategoriaProveedorResponse> CreateCategoriaAsync(CategoriaProveedorRequest request);
    }
}
