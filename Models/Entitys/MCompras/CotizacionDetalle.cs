using ApiMantenimiento.Models.Entitys.MAlmacen;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class CotizacionDetalle
    {
        public int id_cotizacion_detalle { get; set; } // PK
        public int id_cotizacion { get; set; } // FK to Cotizacion
        public int id_material { get; set; } // FK to Alm_Material
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal subtotal { get; set; }

        public Cotizacion Cotizacion { get; set; }
        public Material Material { get; set; }
    }
}
