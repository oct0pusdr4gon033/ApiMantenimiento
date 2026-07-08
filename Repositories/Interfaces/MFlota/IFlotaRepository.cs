using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IFlotaRepository
    {
        Task<IEnumerable<Flota>> ListarAsync();
        Task<Flota> BuscarPorIdAsync(int id);
        Task<Flota> BuscarPorCodigoAsync(string codigo);

        /// <summary>Busca flotas cuyo TipoEquipo del modelo contenga el texto (búsqueda parcial).</summary>
        Task<IEnumerable<Flota>> BuscarPorNombreTipoAsync(string nombreTipo);

        /// <summary>Busca flotas cuyo nombre de modelo contenga el texto (búsqueda parcial).</summary>
        Task<IEnumerable<Flota>> BuscarPorNombreModeloAsync(string nombreModelo);

        Task<Flota> AgregarAsync(Flota flota);
        Task ActualizarAsync(Flota flota);
        Task GuardarAsync();
    }
}
