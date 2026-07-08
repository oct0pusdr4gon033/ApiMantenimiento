using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly MantenimientoDbContext _context;

        public EquipoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query base con todos los includes necesarios para enriquecer el EquipoResponse.
        /// </summary>
        private IQueryable<Equipo> QueryConRelaciones() =>
            _context.Equipos
                .Include(e => e.AreaOperacion)
                .Include(e => e.Flota)
                    .ThenInclude(f => f.ModeloEquipo)
                        .ThenInclude(m => m.Marca)
                .Include(e => e.Flota)
                    .ThenInclude(f => f.ModeloEquipo)
                        .ThenInclude(m => m.TipoEquipo);

        public async Task<IEnumerable<Equipo>> ListarAsync()
            => await QueryConRelaciones().ToListAsync();

        public async Task<Equipo> BuscarPorIdAsync(int id)
            => await QueryConRelaciones().FirstOrDefaultAsync(e => e.id_equipo == id);

        public async Task<Equipo> BuscarPorCodigoAsync(string codigo)
            => await QueryConRelaciones().FirstOrDefaultAsync(e => e.cod_eqp == codigo);

        public async Task<Equipo> BuscarPorPlacaAsync(string placa)
            => await QueryConRelaciones().FirstOrDefaultAsync(e => e.placa_eqp == placa);

        public async Task<IEnumerable<Equipo>> BuscarPorAreaOperacionAsync(string codArea)
            => await QueryConRelaciones()
                .Where(e => e.cod_are_ope == codArea)
                .ToListAsync();

        public async Task<IEnumerable<Equipo>> BuscarPorCodigoFlotaAsync(string codFlota)
            => await QueryConRelaciones()
                .Where(e => e.Flota.cod_flota == codFlota)
                .ToListAsync();

        public async Task<IEnumerable<Equipo>> BuscarPorTipoEquipoAsync(int idTipoEqp)
            => await QueryConRelaciones()
                .Where(e => e.Flota.ModeloEquipo.id_tipo_eqp == idTipoEqp)
                .ToListAsync();

        public async Task<IEnumerable<Equipo>> BuscarPorMarcaAsync(int idMarca)
            => await QueryConRelaciones()
                .Where(e => e.Flota.ModeloEquipo.id_marca == idMarca)
                .ToListAsync();

        public async Task<IEnumerable<Equipo>> BuscarPorModeloAsync(int idModelo)
            => await QueryConRelaciones()
                .Where(e => e.Flota.id_modelo == idModelo)
                .ToListAsync();

        public async Task<Equipo> AgregarAsync(Equipo equipo)
        {
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();
            return equipo;
        }

        public async Task ActualizarAsync(Equipo equipo)
        {
            _context.Equipos.Update(equipo);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
