using System.Collections.Generic;
using System.Threading.Tasks;
using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Repositories.Interfaces.MEmpleados
{
    public interface IExpedienteEmpleadoRepository
    {
        Task<IEnumerable<ExpedienteEmpleado>> ObtenerTodosAsync();
        Task<ExpedienteEmpleado?> ObtenerPorDniAsync(string dni);
        Task<ExpedienteEmpleado?> ObtenerPorCodigoAsync(string codigo);
        Task InsertarExpedienteAsync(ExpedienteEmpleado expediente);
        
        Task<IEnumerable<TipoDocumentoEmpleado>> ObtenerTiposDocumentoAsync();
        Task InsertarTipoDocumentoAsync(TipoDocumentoEmpleado tipoDocumento);
        
        Task<ExpedienteDocumentoEmpleado?> ObtenerDetalleDocumentoAsync(int id);
        Task<IEnumerable<ExpedienteDocumentoEmpleado>> ObtenerDocumentosPorExpedienteAsync(string codigoExp);
        Task InsertarDetalleDocumentoAsync(ExpedienteDocumentoEmpleado documento);
        Task ActualizarDetalleDocumentoAsync(ExpedienteDocumentoEmpleado documento);
        Task EliminarDetalleDocumentoAsync(int id);
        Task GuardarCambiosAsync();
    }
}
