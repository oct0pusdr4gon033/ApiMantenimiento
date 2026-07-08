using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class SistemaEquipoRequest
    {
        [Required]
        [MaxLength(50)]
        public string cod_sist { get; set; }

        [Required]
        [MaxLength(200)]
        public string nombre_sist { get; set; }
    }
}
