using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Repositories.MAlmacen
{
    public class MovimientoInventarioRepository : IMovimientoInventarioRepository
    {
        private readonly MantenimientoDbContext _context;

        public MovimientoInventarioRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovimientoInventarioResponse>> GetByMaterialIdAsync(int idMaterial)
        {
            var query = from mi in _context.MovimientosInventario
                        join v in _context.Vales on mi.origen_referencia equals v.cod_vale into vJoin
                        from v in vJoin.DefaultIfEmpty()
                        join ot in _context.OrdenesTrabajoMant on v.id_ot equals ot.id_ot into otJoin
                        from ot in otJoin.DefaultIfEmpty()
                        where mi.id_material == idMaterial
                        select new MovimientoInventarioResponse
                        {
                            id_movimiento = mi.id_movimiento,
                            id_material = mi.id_material,
                            fecha = mi.fecha,
                            tipo_movimiento = mi.tipo_movimiento,
                            cantidad = mi.cantidad,
                            saldo_stock = mi.saldo_stock,
                            origen_tipo = mi.origen_tipo,
                            origen_referencia = mi.origen_referencia,
                            responsable = mi.responsable,
                            observaciones = mi.observaciones,
                            cod_ot = ot != null ? ot.cod_ot : null
                        };

            return await query.OrderByDescending(x => x.fecha).ToListAsync();
        }

        public async Task<MovimientoInventario> AddAsync(MovimientoInventario movimiento)
        {
            await _context.MovimientosInventario.AddAsync(movimiento);
            await _context.SaveChangesAsync();
            return movimiento;
        }
    }
}
