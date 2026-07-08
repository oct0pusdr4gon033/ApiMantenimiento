using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    /// <summary>Inserta un documento al detalle de un expediente.</summary>
    public class ExpedienteDocumentoRequest
    {
        [Required(ErrorMessage = "El código del expediente es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string CodigoExp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código del tipo de documento es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código de tipo no puede exceder los 10 caracteres.")]
        public string CodTipoDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de registro es obligatoria.")]
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }

        [StringLength(255, ErrorMessage = "La URL del documento no puede exceder los 255 caracteres.")]
        public string DocumentoUrl { get; set; } = string.Empty;
    }
}
