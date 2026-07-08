using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class ActividadSistemaUpdateRequest
    {
        [Required]
        [MaxLength(255)]
        public string nombre_actividad { get; set; }

        public string descripcion { get; set; }

        [Required]
        public int duracion { get; set; }

        [Required]
        [MaxLength(5)]
        [RegularExpression("^(H|MIN)$", ErrorMessage = "La medida de duración debe ser 'H' o 'MIN'.")]
        public string medida_duracion { get; set; }

        public bool estado { get; set; }
    }
}
