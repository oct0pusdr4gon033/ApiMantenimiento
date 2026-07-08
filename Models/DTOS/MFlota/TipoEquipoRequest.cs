using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class TipoEquipoRequest
    {
        [Required(ErrorMessage = "El código del equipo es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string CodigoEquipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del tipo de equipo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombreTipo { get; set; } = string.Empty;
    }
}
