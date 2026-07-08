using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Repositories.Interfaces.MEmpleados
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> ObtenerTodosActivosAsync();
        Task<IEnumerable<Empleado>> ObtenerTodosAsync();
        Task<Empleado> ObtenerPorDniAsync(string dni);
        Task<int> ObtenerConteoPorPrefijoAsync(string prefijo);
        Task AgregarAsync(Empleado empleado);
        Task ActualizarAsync(Empleado empleado);
        Task GuardarCambiosAsync();
    }
}
