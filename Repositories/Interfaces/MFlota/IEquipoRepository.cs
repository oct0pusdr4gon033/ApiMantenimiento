using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IEquipoRepository
    {
        Task<IEnumerable<Equipo>> ListarAsync();
        Task<Equipo> BuscarPorIdAsync(int id);
        Task<Equipo> BuscarPorCodigoAsync(string codigo);
        Task<Equipo> BuscarPorPlacaAsync(string placa);

        // Búsquedas por criterios de la jerarquía
        Task<IEnumerable<Equipo>> BuscarPorAreaOperacionAsync(string codArea);
        Task<IEnumerable<Equipo>> BuscarPorCodigoFlotaAsync(string codFlota);
        Task<IEnumerable<Equipo>> BuscarPorTipoEquipoAsync(int idTipoEqp);
        Task<IEnumerable<Equipo>> BuscarPorMarcaAsync(int idMarca);
        Task<IEnumerable<Equipo>> BuscarPorModeloAsync(int idModelo);

        Task<Equipo> AgregarAsync(Equipo equipo);
        Task ActualizarAsync(Equipo equipo);
        Task GuardarAsync();
    }
}
