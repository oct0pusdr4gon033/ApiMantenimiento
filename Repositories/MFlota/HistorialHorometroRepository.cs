using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class HistorialHorometroRepository : IHistorialHorometroRepository
    {
        private readonly MantenimientoDbContext _context;

        public HistorialHorometroRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistorialHorometros>> ObtenerTodosAsync()
        {
            return await _context.HistorialHorometros
                .OrderByDescending(x => x.fecha_registro)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistorialHorometros>> ObtenerPorEquipoAsync(int idEquipo)
        {
            return await _context.HistorialHorometros
                .Where(x => x.id_equipo == idEquipo)
                .OrderByDescending(x => x.fecha_registro)
                .ToListAsync();
        }

        public async Task<HistorialHorometros> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.HistorialHorometros
                .FirstOrDefaultAsync(x => x.codigo_hist == codigo);
        }

        public async Task AgregarAsync(HistorialHorometros historial)
        {
            await _context.HistorialHorometros.AddAsync(historial);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
