using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class ExpedienteRepository : IExpedienteRepository
    {
        private readonly MantenimientoDbContext _context;

        public ExpedienteRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        /// <summary>Query base que incluye el Equipo y la lista de documentos con su TipoDocumento.</summary>
        private IQueryable<Expediente> QueryConRelaciones() =>
            _context.Expedientes
                .Include(e => e.Equipo)
                .Include(e => e.DetallesDocumento)
                    .ThenInclude(d => d.TipoDocumento);

        public async Task<IEnumerable<Expediente>> ListarAsync()
            => await QueryConRelaciones().ToListAsync();

        public async Task<Expediente> BuscarPorCodigoAsync(string codigo)
            => await QueryConRelaciones()
                .FirstOrDefaultAsync(e => e.codigo_exp == codigo);

        public async Task<Expediente> BuscarPorEquipoAsync(int idEquipo)
            => await QueryConRelaciones()
                .FirstOrDefaultAsync(e => e.id_equipo == idEquipo);

        public async Task<Expediente> AgregarAsync(Expediente expediente)
        {
            _context.Expedientes.Add(expediente);
            await _context.SaveChangesAsync();
            return expediente;
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
