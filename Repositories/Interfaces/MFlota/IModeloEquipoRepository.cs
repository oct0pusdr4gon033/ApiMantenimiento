using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IModeloEquipoRepository
    {
        Task<IEnumerable<ModeloEquipo>> ListarAsync();
        Task<ModeloEquipo> BuscarPorIdAsync(int id);
        Task<ModeloEquipo> BuscarPorNombreAsync(string nombre);
        Task<ModeloEquipo> AgregarAsync(ModeloEquipo modelo);
        Task ActualizarAsync(ModeloEquipo modelo);
        Task GuardarAsync();
    }
}
