using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Técnico o personal asignado a una OT.
    /// Se selecciona del módulo de Empleados usando el plan como referencia de roles.
    /// </summary>
    public class OTPersonal
    {
        public int id_ot_personal { get; set; }
        public int id_ot { get; set; }
        public string dni_empleado { get; set; }
        public int? id_rol { get; set; }

        // Navegación
        public OrdenTrabajo OrdenTrabajo { get; set; }
        public Empleado Empleado { get; set; }
    }
}
