using System;
using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Registro del horómetro de corte de la última intervención de cada PM por equipo.
    /// PK compuesta: (id_equipo, id_detalle_estrg).
    /// Esta tabla es la base del cálculo cíclico de umbrales:
    ///   Próximo umbral = horometro_corte + umbral_PM
    /// 
    /// NOTA DE DISEÑO (sin referencias circulares):
    ///   - Esta tabla referencia OrdenTrabajo (id_ot) mediante FK unidireccional.
    ///   - OrdenTrabajo NO referencia esta tabla — el servicio la consulta en capa de aplicación.
    /// </summary>
    public class PMUltimaIntervencion
    {
        public int id_equipo { get; set; }
        public int id_detalle_estrg { get; set; }

        /// <summary>Horómetro en que se cerró la última OT de este PM. Inicia en 0.</summary>
        public decimal horometro_corte { get; set; } = 0;

        public DateTime? fecha_corte { get; set; }

        /// <summary>FK a la OT que generó este corte (nullable — null si es el estado inicial).</summary>
        public int? id_ot { get; set; }

        // Navegación
        public Equipo Equipo { get; set; }
        public EstrategiaDetalle EstrategiaDetalle { get; set; }
        public OrdenTrabajo OrdenTrabajo { get; set; }
    }
}
