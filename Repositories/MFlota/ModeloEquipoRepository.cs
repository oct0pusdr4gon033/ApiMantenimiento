using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Repositories.MFlota
{
    public class ModeloEquipoRepository : IModeloEquipoRepository
    {
        private readonly MantenimientoDbContext _context;

        public ModeloEquipoRepository(MantenimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ModeloEquipo>> ListarAsync()
            => await _context.ModeloEquipos
                .Include(m => m.Marca)
                .Include(m => m.TipoEquipo)
                .ToListAsync();

        public async Task<ModeloEquipo> BuscarPorIdAsync(int id)
            => await _context.ModeloEquipos
                .Include(m => m.Marca)
                .Include(m => m.TipoEquipo)
                .FirstOrDefaultAsync(m => m.id_modelo == id);

        public async Task<ModeloEquipo> BuscarPorNombreAsync(string nombre)
            => await _context.ModeloEquipos
                .FirstOrDefaultAsync(m => m.nomb_modelo == nombre);

        public async Task<ModeloEquipo> AgregarAsync(ModeloEquipo modelo)
        {
            _context.ModeloEquipos.Add(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }

        public async Task ActualizarAsync(ModeloEquipo modelo)
        {
            _context.ModeloEquipos.Update(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync()
            => await _context.SaveChangesAsync();
    }
}
