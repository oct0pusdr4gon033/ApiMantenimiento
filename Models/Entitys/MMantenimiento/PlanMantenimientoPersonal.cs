using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class PlanMantenimientoPersonal
    {
        public int id_plan_personal { get; set; }
        public int id_plan_mant { get; set; }
        public int id_rol { get; set; }
        public int cantidad { get; set; }

        public PlanMantenimiento PlanMantenimiento { get; set; }
        public Rol Rol { get; set; }
    }
}
