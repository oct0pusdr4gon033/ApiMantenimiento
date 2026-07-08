using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Repositories.Interfaces.MFlota
{
    public interface IExpedienteRepository
    {
        Task<IEnumerable<Expediente>> ListarAsync();
        Task<Expediente> BuscarPorCodigoAsync(string codigo);
        Task<Expediente> BuscarPorEquipoAsync(int idEquipo);
        Task<Expediente> AgregarAsync(Expediente expediente);
        Task GuardarAsync();
    }
}
