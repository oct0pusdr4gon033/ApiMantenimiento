using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Models.Entitys.MSecurity
{
    public class Usuario
    {
        public string dni_empleado { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Empleado Empleado { get; set; }
    }
}
