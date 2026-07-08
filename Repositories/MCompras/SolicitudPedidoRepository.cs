using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MCompras
{
    public class SolicitudPedidoRepository : ISolicitudPedidoRepository
    {
        private readonly MantenimientoDbContext _context;

        public SolicitudPedidoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolicitudPedido>> GetAllAsync()
        {
            return await _context.SolicitudesPedidos
                .Include(s => s.Empleado)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.Material)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.CategoriaMaterial)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.UnidadMedida)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.Proveedor)
                .ToListAsync();
        }

        public async Task<SolicitudPedido> GetByIdAsync(int id)
        {
            return await _context.SolicitudesPedidos
                .Include(s => s.Empleado)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.Material)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.CategoriaMaterial)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.UnidadMedida)
                .Include(s => s.Detalles)
                    .ThenInclude(d => d.Proveedor)
                .FirstOrDefaultAsync(s => s.id_solicitud_pedido == id);
        }

        public async Task<SolicitudPedido> AddAsync(SolicitudPedido solicitud)
        {
            await _context.SolicitudesPedidos.AddAsync(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task UpdateAsync(SolicitudPedido solicitud)
        {
            _context.SolicitudesPedidos.Update(solicitud);
            await _context.SaveChangesAsync();
        }
    }
}
