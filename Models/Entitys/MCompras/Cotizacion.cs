using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class Cotizacion
    {
        public int id_cotizacion { get; set; } // PK
        public string nro_cotizacion { get; set; } // unique generated identifier, e.g. COT-2026-0001
        public int? id_solicitud_pedido { get; set; } // FK to SolicitudPedido (nullable)
        public string ruc_proveedor { get; set; } // FK to Proveedor
        public DateTime fecha_cotizacion { get; set; }
        public string estado { get; set; } // "PENDIENTE", "APROBADO", "RECHAZADO"
        public decimal total { get; set; }

        public SolicitudPedido SolicitudPedido { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<CotizacionDetalle> Detalles { get; set; } = new List<CotizacionDetalle>();
    }
}
