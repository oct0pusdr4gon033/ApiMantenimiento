using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Repositories.Interfaces.MEmpleados
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Rol rol);
        Task ActualizarAsync(Rol rol);
        Task GuardarCambiosAsync();
    }
}
