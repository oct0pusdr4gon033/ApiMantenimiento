using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class ModeloEquipoRequest
    {
        [Required(ErrorMessage = "El ID de la marca es obligatorio.")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El ID del tipo de equipo es obligatorio.")]
        public int IdTipoEqp { get; set; }

        [Required(ErrorMessage = "El nombre del modelo es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres.")]
        public string NombreModelo { get; set; } = string.Empty;
    }
}
