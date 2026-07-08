namespace ApiMantenimiento.Models.Requests.MFlota
{
    public class HistorialHorometroRequest
    {
        public string dni_conductor { get; set; }
        public int id_equipo { get; set; }
        public decimal lectura_anterior { get; set; }
        public decimal lectura_actual { get; set; }
        public decimal horas_operadas { get; set; }
        public string observaciones { get; set; }
    }
}
