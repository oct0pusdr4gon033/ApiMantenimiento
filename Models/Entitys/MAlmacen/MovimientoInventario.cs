using System;

namespace ApiMantenimiento.Models.Entitys.MAlmacen
{
    public class MovimientoInventario
    {
        public int id_movimiento { get; set; }
        public int id_material { get; set; }
        public DateTime fecha { get; set; }
        public string tipo_movimiento { get; set; } // ENTRADA | SALIDA
        public decimal cantidad { get; set; }
        public decimal saldo_stock { get; set; }
        public string origen_tipo { get; set; } // INICIAL | ENTRADA_MANUAL | NOTA_SALIDA | AJUSTE
        public string origen_referencia { get; set; } // cod_vale, Factura, etc.
        public string responsable { get; set; } // Nombre + Apellido1
        public string? observaciones { get; set; }

        // Navegación
        public Material Material { get; set; }
    }
}
