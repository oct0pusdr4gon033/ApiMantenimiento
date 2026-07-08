using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class MarcaEquipoRepository : IMarcaEquipoRepository
    {
        private readonly MantenimientoDbContext _context;

        public MarcaEquipoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MarcaEquipo>> ListarMarcasAsync()
            => await _context.MarcaEquipos.ToListAsync();

        public async Task<MarcaEquipo> BuscarPorIdAsync(int id)
            => await _context.MarcaEquipos.FirstOrDefaultAsync(m => m.id_marca == id);

        public async Task<MarcaEquipo> BuscarPorNombreAsync(string nombre)
            => await _context.MarcaEquipos.FirstOrDefaultAsync(m => m.nombre_marca == nombre);

        public async Task<MarcaEquipo> AgregarAsync(MarcaEquipo marca)
        {
            _context.MarcaEquipos.Add(marca);
            await _context.SaveChangesAsync();
            return marca;
        }

        public async Task<int> ObtenerIdMarcaAsync(string nombre)
        {
            return await _context.MarcaEquipos
                .Where(m => m.nombre_marca == nombre)
                .Select(m => m.id_marca)
                .FirstOrDefaultAsync();
        }

        public async Task ActualizarAsync(MarcaEquipo marca)
        {
            _context.MarcaEquipos.Update(marca);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
