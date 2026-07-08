using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class MarcaEquipoRequest
    {
        [Required(ErrorMessage = "El nombre de la marca es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombreMarca { get; set; } = string.Empty;
    }
}
