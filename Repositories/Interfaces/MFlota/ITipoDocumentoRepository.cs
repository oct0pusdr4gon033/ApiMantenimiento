using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface ITipoDocumentoRepository
    {
        Task<IEnumerable<TipoDocumento>> ListarAsync();
        Task<TipoDocumento> BuscarPorCodigoAsync(string codigo);
        Task<TipoDocumento> AgregarAsync(TipoDocumento tipoDocumento);
        Task ActualizarAsync(TipoDocumento tipoDocumento);
        Task GuardarAsync();
    }
}
