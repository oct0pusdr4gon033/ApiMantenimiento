using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class FlotaRequest
    {
        [Required(ErrorMessage = "El código de flota es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string CodFlota { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del modelo es obligatorio.")]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = "El nombre de la flota es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombreFlota { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El tipo de control no puede exceder los 100 caracteres.")]
        public string TipoControl { get; set; } = string.Empty;
    }
}
