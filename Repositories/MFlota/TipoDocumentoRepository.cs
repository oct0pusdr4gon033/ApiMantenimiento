using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly MantenimientoDbContext _context;

        public TipoDocumentoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoDocumento>> ListarAsync()
            => await _context.TipoDocumentos.ToListAsync();

        public async Task<TipoDocumento> BuscarPorCodigoAsync(string codigo)
            => await _context.TipoDocumentos
                .FirstOrDefaultAsync(t => t.cod_tipo_documento == codigo);

        public async Task<TipoDocumento> AgregarAsync(TipoDocumento tipoDocumento)
        {
            _context.TipoDocumentos.Add(tipoDocumento);
            await _context.SaveChangesAsync();
            return tipoDocumento;
        }

        public async Task ActualizarAsync(TipoDocumento tipoDocumento)
        {
            _context.TipoDocumentos.Update(tipoDocumento);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
