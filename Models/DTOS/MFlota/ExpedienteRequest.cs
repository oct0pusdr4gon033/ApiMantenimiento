using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    /// <summary>Crea un expediente vinculado a un equipo.</summary>
    public class ExpedienteRequest
    {
        [Required(ErrorMessage = "El código del expediente es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string CodigoExp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del equipo es obligatorio.")]
        public int IdEquipo { get; set; }
    }
}
