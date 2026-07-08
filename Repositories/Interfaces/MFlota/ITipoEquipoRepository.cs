using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface ITipoEquipoRepository
    {
        Task<IEnumerable<TipoEquipo>> ListarAsync();
        Task<TipoEquipo> BuscarPorIdAsync(int id);
        Task<TipoEquipo> BuscarPorCodigoAsync(string codigo);
        Task<TipoEquipo> BuscarPorNombreAsync(string nombre);
        Task<IEnumerable<TipoEquipo>> BuscarPorFiltroAsync(string filtro);
        Task<TipoEquipo> AgregarAsync(TipoEquipo tipoEquipo);
        Task ActualizarAsync(TipoEquipo tipoEquipo);
        Task GuardarAsync();
    }
}
