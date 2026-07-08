using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IHistorialHorometroRepository
    {
        Task<IEnumerable<HistorialHorometros>> ObtenerTodosAsync();
        Task<IEnumerable<HistorialHorometros>> ObtenerPorEquipoAsync(int idEquipo);
        Task<HistorialHorometros> ObtenerPorCodigoAsync(string codigo);
        Task AgregarAsync(HistorialHorometros historial);
        Task GuardarCambiosAsync();
    }
}
