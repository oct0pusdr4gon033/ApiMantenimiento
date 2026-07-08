using System;
using System.Collections.Generic;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Entitys.MMantenimiento;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Orden de Trabajo (OT). Puede ser PREVENTIVA (por umbral PM) o CORRECTIVA (falla).
    /// Generación AUTO (sistema) o MANUAL (Jefe de Mantenimiento).
    /// No referencia Man_PMUltimaIntervencion ni Man_CalendarioMantenimiento
    /// para evitar referencias circulares.
    /// </summary>
    public class OrdenTrabajo
    {
        public int id_ot { get; set; }
        public string cod_ot { get; set; }

        // FK → Equipo (datos maestros — sin ciclo)
        public int id_equipo { get; set; }

        // FK → PlanMantenimiento (datos maestros — sin ciclo)
        public int id_plan_mant { get; set; }

        // PREVENTIVA | CORRECTIVA
        public string tipo_ot { get; set; }

        // AUTO | MANUAL
        public string forma_generacion { get; set; }

        // PENDIENTE | ACTIVA | EN_REVISION | CERRADA | INACTIVA
        public string estado { get; set; }

        public decimal horometro_al_momento { get; set; }

        /// <summary>Horómetro registrado al momento de cerrar la OT (null hasta cierre).</summary>
        public decimal? horometro_corte { get; set; }

        public DateTime fecha_creacion { get; set; }

        /// <summary>Se registra automáticamente al pasar a estado CERRADA.</summary>
        public DateTime? fecha_atencion { get; set; }

        public string observaciones { get; set; }

        /// <summary>Usuario Jefe de Mantenimiento que creó la OT manual (null si es AUTO).</summary>
        public string creado_por { get; set; }

        // Campos para OT Correctiva
        public DateTime? hora_intervencion { get; set; }
        public string? descripcion_falla { get; set; }
        public int? id_sistema { get; set; }
        public int? id_subsistema { get; set; }
        public decimal? horometro_falla { get; set; }

        // ── Navegación ────────────────────────────────────────────
        public Equipo Equipo { get; set; }
        public PlanMantenimiento PlanMantenimiento { get; set; }
        public SistemaEquipo? Sistema { get; set; }
        public SubSistemaEquipo? SubSistema { get; set; }

        // PMs incluidos en esta OT (puede ser 1 o varios combinados)
        public ICollection<OTPlanDetalle> PlanesDetalle { get; set; } = new List<OTPlanDetalle>();

        // Actividades copiadas (snapshot)
        public ICollection<OTActividad> Actividades { get; set; } = new List<OTActividad>();

        // Materiales copiados (snapshot)
        public ICollection<OTMaterial> Materiales { get; set; } = new List<OTMaterial>();

        // Personal asignado
        public ICollection<OTPersonal> Personal { get; set; } = new List<OTPersonal>();

        // Relación 1-1 con Vale de Almacén
        public ApiMantenimiento.Models.Entitys.MAlmacen.Vale? Vale { get; set; }
    }
}
