using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Tabla de unión entre PlanMantenimiento y ActividadSistema/EstrategiaDetalle.
    /// PK compuesta natural: (id_plan_mant, id_actividad, id_detalle_estrg)
    /// Los materiales de cada actividad viven en la tabla PlanActividadMaterial (3FN).
    /// </summary>
    public class PlanMantenimientoActividad
    {
        // PK compuesta (sin surrogate)
        public int id_plan_mant { get; set; }
        public int id_actividad { get; set; }
        public int id_detalle_estrg { get; set; }

        // Navegación
        public PlanMantenimiento PlanMantenimiento { get; set; }
        public ActividadSistema ActividadSistema { get; set; }
        public EstrategiaDetalle EstrategiaDetalle { get; set; }

        // Colección de materiales asociados a esta actividad dentro del plan
        public ICollection<PlanActividadMaterial> Materiales { get; set; } = new List<PlanActividadMaterial>();
    }
}
