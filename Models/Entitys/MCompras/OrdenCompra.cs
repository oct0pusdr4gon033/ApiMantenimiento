using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class OrdenCompra
    {
        public int id_orden_compra { get; set; } // PK
        public string nro_orden { get; set; } // unique generated identifier, e.g. OC-2026-0001
        public int? id_cotizacion { get; set; } // FK to Cotizacion (nullable)
        public string ruc_proveedor { get; set; } // FK to Proveedor
        public DateTime fecha_orden { get; set; }
        public string estado { get; set; } // "PENDIENTE", "APROBADO", "RECHAZADO", "RECIBIDO"
        public decimal total { get; set; }

        public Cotizacion Cotizacion { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<OrdenCompraDetalle> Detalles { get; set; } = new List<OrdenCompraDetalle>();
    }
}
