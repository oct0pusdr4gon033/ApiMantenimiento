using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MCompras
{
    public class NotaIngresoRepository : INotaIngresoRepository
    {
        private readonly MantenimientoDbContext _context;

        public NotaIngresoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotaIngreso>> GetAllAsync()
        {
            return await _context.NotasIngresos
                .Include(n => n.OrdenCompra)
                    .ThenInclude(oc => oc.Proveedor)
                .Include(n => n.Detalles)
                    .ThenInclude(d => d.Material)
                .ToListAsync();
        }

        public async Task<NotaIngreso> GetByIdAsync(int id)
        {
            return await _context.NotasIngresos
                .Include(n => n.OrdenCompra)
                    .ThenInclude(oc => oc.Proveedor)
                .Include(n => n.Detalles)
                    .ThenInclude(d => d.Material)
                .FirstOrDefaultAsync(n => n.id_nota_ingreso == id);
        }

        public async Task<NotaIngreso> AddAsync(NotaIngreso nota)
        {
            await _context.NotasIngresos.AddAsync(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        public async Task UpdateAsync(NotaIngreso nota)
        {
            _context.NotasIngresos.Update(nota);
            await _context.SaveChangesAsync();
        }
    }
}
