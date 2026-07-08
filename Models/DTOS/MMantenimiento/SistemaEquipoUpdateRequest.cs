using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class SistemaEquipoUpdateRequest
    {
        [Required]
        [MaxLength(200)]
        public string nombre_sist { get; set; }
    }
}
