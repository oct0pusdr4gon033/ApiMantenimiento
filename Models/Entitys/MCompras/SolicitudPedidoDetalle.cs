using ApiMantenimiento.Models.Entitys.MAlmacen;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class SolicitudPedidoDetalle
    {
        public int id_detalle { get; set; } // PK
        public int id_solicitud_pedido { get; set; } // FK

        // If it exists in warehouse:
        public int? id_material { get; set; } // FK to Alm_Material (nullable)

        // Fields for requesting (if existing, we copy; if new, we fill these in)
        public string cod_materia { get; set; }
        public string nombre { get; set; }
        public int? id_categoria { get; set; } // CategoriaMaterial FK if existing or new
        public int? id_unidad { get; set; } // UnidadMedida FK if existing or new
        public decimal stock_minimo { get; set; }
        public decimal cantidad_pedida { get; set; }
        
        // Provider:
        public string ruc_proveedor { get; set; } // FK to Proveedor (nullable)

        public bool es_nuevo_producto { get; set; }
        public string especificaciones { get; set; } // Specs of the requested item

        public SolicitudPedido SolicitudPedido { get; set; }
        public Material Material { get; set; }
        public CategoriaMaterial CategoriaMaterial { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
