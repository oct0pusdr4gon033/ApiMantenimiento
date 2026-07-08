using System;
using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Configuración global de horas de operación estimadas por flota.
    /// Si id_flota es null, aplica como configuración global del sistema.
    /// Usada para calcular fechas estimadas del calendario de mantenimiento.
    /// </summary>
    public class ConfiguracionFlota
    {
        public int id_configuracion { get; set; }

        /// <summary>Null = configuración global del sistema.</summary>
        public int? id_flota { get; set; }

        /// <summary>Horas de operación estimadas por día. Ej: 12.00</summary>
        public decimal horas_diarias_estimadas { get; set; } = 12;

        public DateTime fecha_actualizacion { get; set; } = DateTime.UtcNow;

        public string actualizado_por { get; set; }

        // Navegación
        public Flota Flota { get; set; }
    }
}
