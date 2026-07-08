using System;

namespace ApiMantenimiento.Models.DTOS.MAlmacen
{
    public class MovimientoInventarioResponse
    {
        public int id_movimiento { get; set; }
        public int id_material { get; set; }
        public DateTime fecha { get; set; }
        public string tipo_movimiento { get; set; } // ENTRADA | SALIDA
        public decimal cantidad { get; set; }
        public decimal saldo_stock { get; set; }
        public string origen_tipo { get; set; }
        public string origen_referencia { get; set; }
        public string responsable { get; set; }
        public string? observaciones { get; set; }
        public string? cod_ot { get; set; } // Opcional, para saber a qué OT está vinculada la salida
    }

    public class StockInflowRequest
    {
        public decimal cantidad { get; set; }
        public string responsable { get; set; } // Nombre + Apellido1
        public string? observaciones { get; set; }
    }
}
