using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IExpedienteDocumentoRepository
    {
        Task<IEnumerable<ExpedienteDocumento>> ListarPorExpedienteAsync(string codigoExp);
        Task<ExpedienteDocumento> BuscarPorIdAsync(int id);
        Task<ExpedienteDocumento> AgregarAsync(ExpedienteDocumento documento);
        Task GuardarAsync();
    }
}
