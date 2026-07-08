using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MAlmacen
{
    public class MaterialRequest
    {
        [Required]
        public int id_unidad { get; set; }

        [Required]
        public int id_categoria { get; set; }

        [Required]
        [MaxLength(50)]
        public string cod_materia { get; set; }

        [Required]
        [MaxLength(500)]
        public string descripcion { get; set; }

        [Required]
        public decimal stock { get; set; }

        [Required]
        [MaxLength(20)]
        public string estado { get; set; } // AGOTADO, STOCK, MINIMO, ACTIVO, INACTIVO
    }

    public class MaterialUpdateRequest
    {
        [Required]
        public int id_unidad { get; set; }

        [Required]
        public int id_categoria { get; set; }

        [Required]
        [MaxLength(50)]
        public string cod_materia { get; set; }

        [Required]
        [MaxLength(500)]
        public string descripcion { get; set; }

        // Note: 'stock' is intentionally omitted here as requested by the user. 
        // It cannot be updated through the standard CRUD update.

        [Required]
        [MaxLength(20)]
        public string estado { get; set; }
    }

    public class MaterialResponse
    {
        public int id_material { get; set; }
        public int id_unidad { get; set; }
        public string nombre_unidad { get; set; }
        public int id_categoria { get; set; }
        public string nombre_categoria { get; set; }
        public string cod_materia { get; set; }
        public string descripcion { get; set; }
        public decimal stock { get; set; }
        public string estado { get; set; }
    }
}
