using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MEmpleados
{
    public class ExpedienteEmpleado
    {
        public string codigo_exp_emp { get; set; }
        public string dni_empleado { get; set; }

        // Relación 1 a 1 con Empleado
        public Empleado Empleado { get; set; }

        // Relación 1 a N con ExpedienteDocumentoEmpleado
        public ICollection<ExpedienteDocumentoEmpleado> DetallesDocumento { get; set; }
    }
}
