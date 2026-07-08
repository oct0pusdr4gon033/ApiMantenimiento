using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MCompras
{
    public class OrdenCompraRequest
    {
        public int? id_cotizacion { get; set; }
        public string ruc_proveedor { get; set; }
        public List<OrdenCompraDetalleRequest> detalles { get; set; } = new List<OrdenCompraDetalleRequest>();
    }

    public class OrdenCompraDetalleRequest
    {
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
    }

    public class OrdenCompraResponse
    {
        public int id_orden_compra { get; set; }
        public string nro_orden { get; set; }
        public int? id_cotizacion { get; set; }
        public string nro_cotizacion { get; set; }
        public string ruc_proveedor { get; set; }
        public string razon_social_proveedor { get; set; }
        public DateTime fecha_orden { get; set; }
        public string estado { get; set; }
        public decimal total { get; set; }
        public List<OrdenCompraDetalleResponse> detalles { get; set; } = new List<OrdenCompraDetalleResponse>();
    }

    public class OrdenCompraDetalleResponse
    {
        public int id_orden_detalle { get; set; }
        public int id_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal subtotal { get; set; }
    }
}
