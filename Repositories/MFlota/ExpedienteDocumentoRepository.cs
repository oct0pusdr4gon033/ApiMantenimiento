using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class ExpedienteDocumentoRepository : IExpedienteDocumentoRepository
    {
        private readonly MantenimientoDbContext _context;

        public ExpedienteDocumentoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        private IQueryable<ExpedienteDocumento> QueryConRelaciones() =>
            _context.ExpedienteDocumentos
                .Include(d => d.Expediente)
                .Include(d => d.TipoDocumento);

        public async Task<IEnumerable<ExpedienteDocumento>> ListarPorExpedienteAsync(string codigoExp)
            => await QueryConRelaciones()
                .Where(d => d.codigo_exp == codigoExp)
                .ToListAsync();

        public async Task<ExpedienteDocumento> BuscarPorIdAsync(int id)
            => await QueryConRelaciones()
                .FirstOrDefaultAsync(d => d.id_expediente_documento == id);

        public async Task<ExpedienteDocumento> AgregarAsync(ExpedienteDocumento documento)
        {
            _context.ExpedienteDocumentos.Add(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
