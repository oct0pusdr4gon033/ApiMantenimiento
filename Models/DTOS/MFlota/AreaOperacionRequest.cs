using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class AreaOperacionRequest
    {
        [Required(ErrorMessage = "El código de área es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código no puede superar los 10 caracteres.")]
        public string CodigoArea { get; set; }

        [Required(ErrorMessage = "El nombre del área es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre no puede superar los 20 caracteres.")]
        public string NombreArea { get; set; }
    }
}
