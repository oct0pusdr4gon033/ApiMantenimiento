using System;

namespace ApiMantenimiento.Models.Entitys.MAlmacen
{
    public class HistorialPrecio
    {
        public int id_historial { get; set; }
        public int id_material { get; set; }
        public decimal precio { get; set; }
        public DateTime fecha_registro { get; set; }

        public Material Material { get; set; }
    }
}
