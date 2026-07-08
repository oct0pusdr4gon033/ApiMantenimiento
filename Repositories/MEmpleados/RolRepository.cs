using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MEmpleados
{
    public class RolRepository : IRolRepository
    {
        private readonly MantenimientoDbContext _context;

        public RolRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> ObtenerTodosAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> ObtenerPorIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task AgregarAsync(Rol rol)
        {
            await _context.Roles.AddAsync(rol);
        }

        public Task ActualizarAsync(Rol rol)
        {
            _context.Roles.Update(rol);
            return Task.CompletedTask;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
