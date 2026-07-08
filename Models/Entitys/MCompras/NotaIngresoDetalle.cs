using ApiMantenimiento.Models.Entitys.MAlmacen;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class NotaIngresoDetalle
    {
        public int id_nota_detalle { get; set; } // PK
        public int id_nota_ingreso { get; set; } // FK to NotaIngreso
        public int id_material { get; set; } // FK to Alm_Material
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }

        public NotaIngreso NotaIngreso { get; set; }
        public Material Material { get; set; }
    }
}
