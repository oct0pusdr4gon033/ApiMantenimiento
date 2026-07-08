using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class TipoEquipoRepository : ITipoEquipoRepository
    {
        private readonly MantenimientoDbContext _context;

        public TipoEquipoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoEquipo>> ListarAsync()
            => await _context.TipoEquipos.ToListAsync();

        public async Task<TipoEquipo> BuscarPorIdAsync(int id)
            => await _context.TipoEquipos.FirstOrDefaultAsync(t => t.id_tipo_eqp == id);

        public async Task<TipoEquipo> BuscarPorCodigoAsync(string codigo)
            => await _context.TipoEquipos.FirstOrDefaultAsync(t => t.cod_equipo == codigo);

        public async Task<TipoEquipo> BuscarPorNombreAsync(string nombre)
            => await _context.TipoEquipos.FirstOrDefaultAsync(t => t.nombre_tipo == nombre);

        public async Task<IEnumerable<TipoEquipo>> BuscarPorFiltroAsync(string filtro)
        {
            return await _context.TipoEquipos
                .Where(t => t.cod_equipo.Contains(filtro) || t.nombre_tipo.Contains(filtro))
                .ToListAsync();
        }

        public async Task<TipoEquipo> AgregarAsync(TipoEquipo tipoEquipo)
        {
            _context.TipoEquipos.Add(tipoEquipo);
            await _context.SaveChangesAsync();
            return tipoEquipo;
        }

        public async Task ActualizarAsync(TipoEquipo tipoEquipo)
        {
            _context.TipoEquipos.Update(tipoEquipo);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
