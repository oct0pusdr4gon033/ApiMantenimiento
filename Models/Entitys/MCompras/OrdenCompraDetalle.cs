using ApiMantenimiento.Models.Entitys.MAlmacen;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class OrdenCompraDetalle
    {
        public int id_orden_detalle { get; set; } // PK
        public int id_orden_compra { get; set; } // FK to OrdenCompra
        public int id_material { get; set; } // FK to Alm_Material
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal subtotal { get; set; }

        public OrdenCompra OrdenCompra { get; set; }
        public Material Material { get; set; }
    }
}
