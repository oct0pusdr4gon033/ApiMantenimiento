using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MCompras
{
    public class NotaIngresoRequest
    {
        public int id_orden_compra { get; set; }
        public string observaciones { get; set; }
        public List<NotaIngresoDetalleRequest> detalles { get; set; } = new List<NotaIngresoDetalleRequest>();
    }

    public class NotaIngresoDetalleRequest
    {
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
    }

    public class NotaIngresoResponse
    {
        public int id_nota_ingreso { get; set; }
        public string nro_nota { get; set; }
        public int id_orden_compra { get; set; }
        public string nro_orden_compra { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string estado { get; set; }
        public string observaciones { get; set; }
        public List<NotaIngresoDetalleResponse> detalles { get; set; } = new List<NotaIngresoDetalleResponse>();
    }

    public class NotaIngresoDetalleResponse
    {
        public int id_nota_detalle { get; set; }
        public int id_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio_unitario { get; set; }
    }
}
