using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MEmpleados
{
    public class TipoDocumentoEmpleadoRequest
    {
        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(10, ErrorMessage = "No puede exceder los 10 caracteres.")]
        public string CodTipoDocumentoEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "No puede exceder los 100 caracteres.")]
        public string NombreTipo { get; set; } = string.Empty;
    }
}
