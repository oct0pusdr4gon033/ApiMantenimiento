using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MEmpleados
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly MantenimientoDbContext _context;

        public EmpleadoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodosActivosAsync()
        {
            return await _context.Empleados
                .Include(e => e.Rol)
                .Where(e => e.estado == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
        {
            return await _context.Empleados
                .Include(e => e.Rol)
                .ToListAsync();
        }

        public async Task<Empleado> ObtenerPorDniAsync(string dni)
        {
            return await _context.Empleados
                .Include(e => e.Rol)
                .FirstOrDefaultAsync(e => e.dni_empleado == dni);
        }

        public async Task<int> ObtenerConteoPorPrefijoAsync(string prefijo)
        {
            return await _context.Empleados
                .Where(e => e.codigo_empleado.StartsWith(prefijo))
                .CountAsync();
        }

        public async Task AgregarAsync(Empleado empleado)
        {
            await _context.Empleados.AddAsync(empleado);
        }

        public Task ActualizarAsync(Empleado empleado)
        {
            _context.Empleados.Update(empleado);
            return Task.CompletedTask;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
