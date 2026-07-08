using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MCompras
{
    public class SolicitudPedidoRequest
    {
        public string dni_empleado { get; set; }
        public List<SolicitudPedidoDetalleRequest> detalles { get; set; } = new List<SolicitudPedidoDetalleRequest>();
    }

    public class SolicitudPedidoDetalleRequest
    {
        public int? id_material { get; set; }
        public string cod_materia { get; set; }
        public string nombre { get; set; }
        public int? id_categoria { get; set; }
        public int? id_unidad { get; set; }
        public decimal stock_minimo { get; set; }
        public decimal cantidad_pedida { get; set; }
        public string ruc_proveedor { get; set; }
        
        // For inline registration of a provider directly from the SOLPED UI
        public ProveedorRequest nuevo_proveedor { get; set; }

        public bool es_nuevo_producto { get; set; }
        public string especificaciones { get; set; }
    }

    public class SolicitudPedidoResponse
    {
        public int id_solicitud_pedido { get; set; }
        public string cod_solicitud { get; set; }
        public string dni_empleado { get; set; }
        public string nombre_empleado { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string estado { get; set; }
        public List<SolicitudPedidoDetalleResponse> detalles { get; set; } = new List<SolicitudPedidoDetalleResponse>();
    }

    public class SolicitudPedidoDetalleResponse
    {
        public int id_detalle { get; set; }
        public int? id_material { get; set; }
        public string cod_materia { get; set; }
        public string nombre { get; set; }
        public int? id_categoria { get; set; }
        public string nombre_categoria { get; set; }
        public int? id_unidad { get; set; }
        public string nombre_unidad { get; set; }
        public decimal stock_minimo { get; set; }
        public decimal cantidad_pedida { get; set; }
        public string ruc_proveedor { get; set; }
        public string razon_social_proveedor { get; set; }
        public bool es_nuevo_producto { get; set; }
        public string especificaciones { get; set; }
    }
}
