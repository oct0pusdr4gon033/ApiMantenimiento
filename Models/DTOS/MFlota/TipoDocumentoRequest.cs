using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class TipoDocumentoRequest
    {
        [Required(ErrorMessage = "El código del tipo de documento es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código no puede exceder los 10 caracteres.")]
        public string CodTipoDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del tipo de documento es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombreTipo { get; set; } = string.Empty;
    }
}
