using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IMarcaEquipoRepository
    {
        Task<IEnumerable<MarcaEquipo>> ListarMarcasAsync();
        Task<MarcaEquipo> BuscarPorIdAsync(int id);
        Task<MarcaEquipo> BuscarPorNombreAsync(string nombre);
        Task<MarcaEquipo> AgregarAsync(MarcaEquipo marca);
        Task<int> ObtenerIdMarcaAsync(string nombre);
        Task ActualizarAsync(MarcaEquipo marca);
        Task GuardarAsync();
    }
}
