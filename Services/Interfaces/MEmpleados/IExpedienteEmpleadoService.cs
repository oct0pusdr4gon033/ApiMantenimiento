using System.Collections.Generic;
using System.Threading.Tasks;
using ApiMantenimiento.Models.DTOS.MEmpleados;

namespace ApiMantenimiento.Services.Interfaces.MEmpleados
{
    public interface IExpedienteEmpleadoService
    {
        Task<IEnumerable<ExpedienteEmpleadoResponse>> ObtenerTodosAsync();
        Task<ExpedienteEmpleadoResponse> ObtenerPorDniAsync(string dni);
        Task<ExpedienteEmpleadoResponse> ObtenerPorCodigoAsync(string codigo);
        Task<ExpedienteEmpleadoResponse> CrearExpedienteAsync(ExpedienteEmpleadoRequest request);

        Task<IEnumerable<TipoDocumentoEmpleadoResponse>> ObtenerTiposDocumentoAsync();
        Task<TipoDocumentoEmpleadoResponse> CrearTipoDocumentoAsync(TipoDocumentoEmpleadoRequest request);

        Task<IEnumerable<ExpedienteDocumentoEmpleadoResponse>> ObtenerDocumentosAsync(string codigoExp);
        Task<ExpedienteDocumentoEmpleadoResponse> AgregarDocumentoAsync(ExpedienteDocumentoEmpleadoRequest request);
        Task<ExpedienteDocumentoEmpleadoResponse> ActualizarDocumentoAsync(int id, ExpedienteDocumentoEmpleadoRequest request);
        Task EliminarDocumentoAsync(int idExpedienteDocumento);
    }
}
