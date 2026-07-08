using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class NotaIngreso
    {
        public int id_nota_ingreso { get; set; } // PK
        public string nro_nota { get; set; } // unique generated identifier, e.g. NI-2026-0001
        public int id_orden_compra { get; set; } // FK to OrdenCompra
        public DateTime fecha_ingreso { get; set; }
        public string estado { get; set; } // "PROCESADO", "ANULADO"
        public string observaciones { get; set; }

        public OrdenCompra OrdenCompra { get; set; }
        public ICollection<NotaIngresoDetalle> Detalles { get; set; } = new List<NotaIngresoDetalle>();
    }
}
