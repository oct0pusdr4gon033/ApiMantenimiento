using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MAlmacen
{
    public class CategoriaMaterialRequest
    {
        [Required]
        [MaxLength(20)]
        public string cod_cat { get; set; }

        [Required]
        [MaxLength(150)]
        public string nombre_categoria { get; set; }
    }

    public class CategoriaMaterialResponse
    {
        public int id_categoria { get; set; }
        public string cod_cat { get; set; }
        public string nombre_categoria { get; set; }
    }
}
