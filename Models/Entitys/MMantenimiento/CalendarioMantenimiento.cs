using System;
using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Proyección del calendario de mantenimiento por equipo/PM.
    /// Solo lectura — se recalcula automáticamente al registrar horómetros.
    /// Se mantiene una fila activa por (id_equipo, id_detalle_estrg).
    /// Al ejecutarse, la fila se marca como ejecutado=true y se crea una nueva proyección.
    /// </summary>
    public class CalendarioMantenimiento
    {
        public int id_calendario { get; set; }

        public int id_equipo { get; set; }
        public int id_detalle_estrg { get; set; }

        /// <summary>Horómetro desde el que se calculó esta proyección.</summary>
        public decimal horometro_base { get; set; }

        /// <summary>Fecha en que se realizó el cálculo.</summary>
        public DateTime fecha_base { get; set; }

        /// <summary>Horas diarias estimadas usadas para el cálculo (de ConfiguracionFlota).</summary>
        public decimal horas_diarias_usadas { get; set; }

        /// <summary>Fecha estimada de alcance del umbral.</summary>
        public DateTime fecha_estimada { get; set; }

        /// <summary>OT generada para este PM (null mientras no se haya generado).</summary>
        public int? id_ot { get; set; }

        /// <summary>True cuando la OT asociada está CERRADA.</summary>
        public bool ejecutado { get; set; } = false;

        /// <summary>Fecha real de ejecución (al cerrarse la OT).</summary>
        public DateTime? fecha_real_ejecucion { get; set; }

        // Navegación
        public Equipo Equipo { get; set; }
        public EstrategiaDetalle EstrategiaDetalle { get; set; }
        public OrdenTrabajo OrdenTrabajo { get; set; }
    }
}
