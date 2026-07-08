using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MCompras
{
    public class CotizacionRepository : ICotizacionRepository
    {
        private readonly MantenimientoDbContext _context;

        public CotizacionRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cotizacion>> GetAllAsync()
        {
            return await _context.Cotizaciones
                .Include(c => c.SolicitudPedido)
                .Include(c => c.Proveedor)
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.Material)
                .ToListAsync();
        }

        public async Task<Cotizacion> GetByIdAsync(int id)
        {
            return await _context.Cotizaciones
                .Include(c => c.SolicitudPedido)
                .Include(c => c.Proveedor)
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.Material)
                .FirstOrDefaultAsync(c => c.id_cotizacion == id);
        }

        public async Task<Cotizacion> AddAsync(Cotizacion cotizacion)
        {
            await _context.Cotizaciones.AddAsync(cotizacion);
            await _context.SaveChangesAsync();
            return cotizacion;
        }

        public async Task UpdateAsync(Cotizacion cotizacion)
        {
            _context.Cotizaciones.Update(cotizacion);
            await _context.SaveChangesAsync();
        }
    }
}
