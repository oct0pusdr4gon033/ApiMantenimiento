using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;

using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class AreaOperacionRepository : IAreaOperacionRepository
    {
        private readonly MantenimientoDbContext _context;
        public AreaOperacionRepository(MantenimientoDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AreaOperacion>> ListarAreasAsync()
        {
            return await _context.AreaOperaciones.ToListAsync();
        }
        public async Task<AreaOperacion> BuscarPorCodigoAsync(string codigoArea)
        {
            return await _context.AreaOperaciones.FirstOrDefaultAsync(a => a.cod_area_ope == codigoArea);
        }
        public async Task<AreaOperacion> BuscarPorNombreAsync(string nombreArea)
        {
            return await _context.AreaOperaciones.FirstOrDefaultAsync(a => a.nomb_area == nombreArea);
        }
        public async Task<AreaOperacion> AgregarAsync(AreaOperacion areaOperacion)
        {
            _context.AreaOperaciones.Add(areaOperacion);
            await _context.SaveChangesAsync();
            return areaOperacion;
        }
        public async Task ActualizarAsync(AreaOperacion areaOperacion)
        {
            _context.AreaOperaciones.Update(areaOperacion);
            await _context.SaveChangesAsync();
        }
        public async Task GuardarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
