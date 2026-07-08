using System;
using System.Collections.Generic;
using ApiMantenimiento.Models.Entitys.MMantenimiento;

namespace ApiMantenimiento.Models.Entitys.MAlmacen
{
    public class Vale
    {
        public int id_vale { get; set; }
        public string cod_vale { get; set; }
        public int? id_ot { get; set; }

        public string estado { get; set; } // PENDIENTE | DESPACHADO
        public DateTime fecha_creacion { get; set; }
        public DateTime? fecha_despacho { get; set; }
        public string solicitado_por { get; set; } // Nombre + Apellido1
        public string? despachado_por { get; set; } // Nombre + Apellido1
        public string? observaciones { get; set; }

        // Navegación
        public OrdenTrabajo? OrdenTrabajo { get; set; }
        public ICollection<ValeMaterial> Materiales { get; set; } = new List<ValeMaterial>();
    }
}
