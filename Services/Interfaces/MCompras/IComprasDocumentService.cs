using ApiMantenimiento.Models.DTOS.MCompras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.Interfaces.MCompras
{
    public interface IComprasDocumentService
    {
        // Solicitud de Pedido (SOLPED)
        Task<IEnumerable<SolicitudPedidoResponse>> GetAllSolpedsAsync();
        Task<SolicitudPedidoResponse> GetSolpedByIdAsync(int id);
        Task<SolicitudPedidoResponse> CrearSolicitudPedidoAsync(SolicitudPedidoRequest request);
        Task<CotizacionResponse> AprobarSolicitudPedidoAsync(int id);

        // Cotizaciones
        Task<IEnumerable<CotizacionResponse>> GetAllCotizacionesAsync();
        Task<CotizacionResponse> GetCotizacionByIdAsync(int id);
        Task<CotizacionResponse> CrearCotizacionAsync(CotizacionRequest request);
        Task<CotizacionResponse> ActualizarCotizacionAsync(int id, CotizacionUpdateRequest request);
        Task<OrdenCompraResponse> AprobarCotizacionAsync(int id);

        // Ordenes de Compra
        Task<IEnumerable<OrdenCompraResponse>> GetAllOrdenesCompraAsync();
        Task<OrdenCompraResponse> GetOrdenCompraByIdAsync(int id);
        Task<OrdenCompraResponse> CrearOrdenCompraAsync(OrdenCompraRequest request);
        Task<OrdenCompraResponse> AprobarOrdenCompraAsync(int id);
        Task<NotaIngresoResponse> ProcesarNotaIngresoAsync(int idOrdenCompra, string observaciones);

        // Notas de Ingreso
        Task<IEnumerable<NotaIngresoResponse>> GetAllNotasIngresoAsync();
        Task<NotaIngresoResponse> GetNotaIngresoByIdAsync(int id);
    }
}
