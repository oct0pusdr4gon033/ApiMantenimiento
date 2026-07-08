using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MCompras
{
    public class CotizacionRequest
    {
        public int? id_solicitud_pedido { get; set; }
        public string ruc_proveedor { get; set; }
        public List<CotizacionDetalleRequest> detalles { get; set; } = new List<CotizacionDetalleRequest>();
    }

    public class CotizacionDetalleRequest
    {
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
    }

    public class CotizacionResponse
    {
        public int id_cotizacion { get; set; }
        public string nro_cotizacion { get; set; }
        public int? id_solicitud_pedido { get; set; }
        public string cod_solicitud_pedido { get; set; }
        public string ruc_proveedor { get; set; }
        public string razon_social_proveedor { get; set; }
        public DateTime fecha_cotizacion { get; set; }
        public string estado { get; set; }
        public decimal total { get; set; }
        public List<CotizacionDetalleResponse> detalles { get; set; } = new List<CotizacionDetalleResponse>();
    }

    public class CotizacionDetalleResponse
    {
        public int id_cotizacion_detalle { get; set; }
        public int id_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal subtotal { get; set; }
    }
    public class CotizacionUpdateRequest
    {
        public List<CotizacionDetalleUpdateRequest> detalles { get; set; } = new List<CotizacionDetalleUpdateRequest>();
    }

    public class CotizacionDetalleUpdateRequest
    {
        public int id_cotizacion_detalle { get; set; }
        public decimal precio_unitario { get; set; }
    }
}
