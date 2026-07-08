using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IAreaOperacionRepository
    {
        Task<IEnumerable<AreaOperacion>> ListarAreasAsync();
        Task<AreaOperacion> BuscarPorCodigoAsync(string codigo);
        Task<AreaOperacion> BuscarPorNombreAsync(string nombre);
        Task<AreaOperacion> AgregarAsync(AreaOperacion area);
        Task ActualizarAsync(AreaOperacion area);
        Task GuardarAsync();
    }
}
