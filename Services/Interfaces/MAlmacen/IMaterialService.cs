using ApiMantenimiento.Models.DTOS.MAlmacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MAlmacen
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialResponse>> GetAllAsync();
        
        // Advanced search service method
        Task<IEnumerable<MaterialResponse>> BuscarMaterialesAsync(string cod_materia, string nombre, string estado, int? id_unidad, int? id_categoria);
        
        Task<MaterialResponse> CreateAsync(MaterialRequest request);
        Task<MaterialResponse> UpdateAsync(int id, MaterialUpdateRequest request);
        Task<IEnumerable<MovimientoInventarioResponse>> GetKardexAsync(int idMaterial);
        Task<MaterialResponse> RegistrarEntradaStockAsync(int idMaterial, StockInflowRequest request);
    }
}
