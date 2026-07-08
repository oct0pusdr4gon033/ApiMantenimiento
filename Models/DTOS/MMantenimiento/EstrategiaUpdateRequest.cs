using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class EstrategiaUpdateRequest
    {
        [Required]
        public string titulo_estrategia { get; set; }
        
        [Required]
        public string estado { get; set; }
    }
}
