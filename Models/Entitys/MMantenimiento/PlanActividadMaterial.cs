using ApiMantenimiento.Models.Entitys.MAlmacen;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Tabla de unión: relaciona un material (con cantidad) con una actividad específica
    /// dentro de un plan de mantenimiento.
    /// PK compuesta: (id_plan_mant, id_actividad, id_detalle_estrg, id_material)
    /// </summary>
    public class PlanActividadMaterial
    {
        // Parte de PK compuesta y FK → Man_PlanMantenimientoActividad
        public int id_plan_mant { get; set; }
        public int id_actividad { get; set; }
        public int id_detalle_estrg { get; set; }

        // Parte de PK compuesta y FK → Material
        public int id_material { get; set; }

        public decimal cantidad { get; set; }

        // Navegación
        public PlanMantenimientoActividad PlanMantenimientoActividad { get; set; }
        public Material Material { get; set; }
    }
}
