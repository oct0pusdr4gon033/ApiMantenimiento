using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MCompras
{
    public class OrdenCompraRepository : IOrdenCompraRepository
    {
        private readonly MantenimientoDbContext _context;

        public OrdenCompraRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenCompra>> GetAllAsync()
        {
            return await _context.OrdenesCompras
                .Include(o => o.Cotizacion)
                .Include(o => o.Proveedor)
                .Include(o => o.Detalles)
                    .ThenInclude(d => d.Material)
                .ToListAsync();
        }

        public async Task<OrdenCompra> GetByIdAsync(int id)
        {
            return await _context.OrdenesCompras
                .Include(o => o.Cotizacion)
                .Include(o => o.Proveedor)
                .Include(o => o.Detalles)
                    .ThenInclude(d => d.Material)
                .FirstOrDefaultAsync(o => o.id_orden_compra == id);
        }

        public async Task<OrdenCompra> AddAsync(OrdenCompra orden)
        {
            await _context.OrdenesCompras.AddAsync(orden);
            await _context.SaveChangesAsync();
            return orden;
        }

        public async Task UpdateAsync(OrdenCompra orden)
        {
            _context.OrdenesCompras.Update(orden);
            await _context.SaveChangesAsync();
        }
    }
}
