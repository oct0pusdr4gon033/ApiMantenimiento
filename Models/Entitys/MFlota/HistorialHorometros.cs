using System.Globalization;
using ApiMantenimiento.Models.Entitys.MEmpleados;

namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class HistorialHorometros
    {
        public string codigo_hist { get; set; }

        //un conductor puede tener varios registros de historial
        //pero cada registro de historial está asociado a un solo conductor
        public string dni_conductor { get; set; }

        //un equipo puede tener varios registros de historial,
        //pero cada registro de historial está asociado a un solo equipo
        public int id_equipo { get; set; }  
        public DateTime fecha_registro { get; set; }
        public decimal lectura_anterior { get; set; }
        public decimal lectura_actual { get; set; }
        public decimal horas_operadas { get; set; }
        public string observaciones { get; set; }
        
        public Empleado Empleado { get; set; }
    }
}
