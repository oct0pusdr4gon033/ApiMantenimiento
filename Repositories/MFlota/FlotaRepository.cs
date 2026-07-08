using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class FlotaRepository : IFlotaRepository
    {
        private readonly MantenimientoDbContext _context;

        public FlotaRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        /// <summary>Query base con todos los includes necesarios para enriquecer FlotaResponse.</summary>
        private IQueryable<Flota> QueryConRelaciones() =>
            _context.Flotas
                .Include(f => f.ModeloEquipo)
                    .ThenInclude(m => m.Marca)
                .Include(f => f.ModeloEquipo)
                    .ThenInclude(m => m.TipoEquipo);

        public async Task<IEnumerable<Flota>> ListarAsync()
            => await QueryConRelaciones().ToListAsync();

        public async Task<Flota> BuscarPorIdAsync(int id)
            => await QueryConRelaciones()
                .FirstOrDefaultAsync(f => f.id_flota == id);

        public async Task<Flota> BuscarPorCodigoAsync(string codigo)
            => await QueryConRelaciones()
                .FirstOrDefaultAsync(f => f.cod_flota == codigo);

        public async Task<IEnumerable<Flota>> BuscarPorNombreTipoAsync(string nombreTipo)
            => await QueryConRelaciones()
                .Where(f => f.ModeloEquipo.TipoEquipo.nombre_tipo.Contains(nombreTipo))
                .ToListAsync();

        public async Task<IEnumerable<Flota>> BuscarPorNombreModeloAsync(string nombreModelo)
            => await QueryConRelaciones()
                .Where(f => f.ModeloEquipo.nomb_modelo.Contains(nombreModelo))
                .ToListAsync();

        public async Task<Flota> AgregarAsync(Flota flota)
        {
            _context.Flotas.Add(flota);
            await _context.SaveChangesAsync();
            return flota;
        }

        public async Task ActualizarAsync(Flota flota)
        {
            _context.Flotas.Update(flota);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
