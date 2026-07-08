using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MEmpleados
{
    public class ExpedienteEmpleadoRepository : IExpedienteEmpleadoRepository
    {
        private readonly MantenimientoDbContext _context;

        public ExpedienteEmpleadoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExpedienteEmpleado>> ObtenerTodosAsync()
        {
            return await _context.ExpedienteEmpleados
                .Include(x => x.Empleado)
                .Include(x => x.DetallesDocumento)
                    .ThenInclude(d => d.TipoDocumentoEmpleado)
                .ToListAsync();
        }

        public async Task<ExpedienteEmpleado?> ObtenerPorDniAsync(string dni)
        {
            return await _context.ExpedienteEmpleados
                .Include(x => x.Empleado)
                .Include(x => x.DetallesDocumento)
                    .ThenInclude(d => d.TipoDocumentoEmpleado)
                .FirstOrDefaultAsync(x => x.dni_empleado == dni);
        }

        public async Task<ExpedienteEmpleado?> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.ExpedienteEmpleados
                .Include(x => x.Empleado)
                .Include(x => x.DetallesDocumento)
                    .ThenInclude(d => d.TipoDocumentoEmpleado)
                .FirstOrDefaultAsync(x => x.codigo_exp_emp == codigo);
        }

        public async Task InsertarExpedienteAsync(ExpedienteEmpleado expediente)
        {
            await _context.ExpedienteEmpleados.AddAsync(expediente);
        }

        public async Task<IEnumerable<TipoDocumentoEmpleado>> ObtenerTiposDocumentoAsync()
        {
            return await _context.TipoDocumentoEmpleados.ToListAsync();
        }

        public async Task InsertarTipoDocumentoAsync(TipoDocumentoEmpleado tipoDocumento)
        {
            await _context.TipoDocumentoEmpleados.AddAsync(tipoDocumento);
        }

        public async Task<ExpedienteDocumentoEmpleado?> ObtenerDetalleDocumentoAsync(int id)
        {
            return await _context.ExpedienteDocumentoEmpleados
                .Include(x => x.TipoDocumentoEmpleado)
                .FirstOrDefaultAsync(x => x.id_exp_doc_emp == id);
        }

        public async Task<IEnumerable<ExpedienteDocumentoEmpleado>> ObtenerDocumentosPorExpedienteAsync(string codigoExp)
        {
            return await _context.ExpedienteDocumentoEmpleados
                .Include(x => x.TipoDocumentoEmpleado)
                .Where(x => x.codigo_exp_emp == codigoExp)
                .OrderByDescending(x => x.fecha_registro)
                .ToListAsync();
        }

        public async Task InsertarDetalleDocumentoAsync(ExpedienteDocumentoEmpleado documento)
        {
            await _context.ExpedienteDocumentoEmpleados.AddAsync(documento);
        }

        public async Task ActualizarDetalleDocumentoAsync(ExpedienteDocumentoEmpleado documento)
        {
            _context.ExpedienteDocumentoEmpleados.Update(documento);
        }

        public async Task EliminarDetalleDocumentoAsync(int id)
        {
            var documento = await _context.ExpedienteDocumentoEmpleados.FindAsync(id);
            if (documento != null)
            {
                _context.ExpedienteDocumentoEmpleados.Remove(documento);
            }
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
