using System;
using System.Collections.Generic;
using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class SolicitudPedido
    {
        public int id_solicitud_pedido { get; set; } // PK
        public string cod_solicitud { get; set; } // unique generated identifier, e.g. SOL-2026-0001
        public string dni_empleado { get; set; } // FK to Empleado (char(8))
        public DateTime fecha_creacion { get; set; }
        public string estado { get; set; } // "PENDIENTE", "APROBADO", "RECHAZADO"

        public Empleado Empleado { get; set; }
        public ICollection<SolicitudPedidoDetalle> Detalles { get; set; } = new List<SolicitudPedidoDetalle>();
    }
}
