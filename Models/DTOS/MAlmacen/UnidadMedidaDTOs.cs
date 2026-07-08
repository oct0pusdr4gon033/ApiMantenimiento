using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MAlmacen
{
    public class UnidadMedidaRequest
    {
        [Required]
        [MaxLength(100)]
        public string nombre_unidad { get; set; }

        [Required]
        [MaxLength(10)]
        public string abreviatura { get; set; }
    }

    public class UnidadMedidaResponse
    {
        public int id_unidad { get; set; }
        public string nombre_unidad { get; set; }
        public string abreviatura { get; set; }
    }
}
