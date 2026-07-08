namespace ApiMantenimiento.Models.Responses.MEmpleados
{
    public class EmpleadoResponse
    {
        public string codigo_empleado { get; set; }
        public string dni_empleado { get; set; }
        public int id_rol { get; set; }
        public string nombre_rol { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telf { get; set; }
        public string email { get; set; }
        public bool estado { get; set; }
    }
}
