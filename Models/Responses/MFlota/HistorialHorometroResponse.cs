namespace ApiMantenimiento.Models.Responses.MFlota
{
    public class HistorialHorometroResponse
    {
        public string codigo_hist { get; set; }
        public string dni_conductor { get; set; }
        public int id_equipo { get; set; }
        public DateTime fecha_registro { get; set; }
        public decimal lectura_anterior { get; set; }
        public decimal lectura_actual { get; set; }
        public decimal horas_operadas { get; set; }
        public string observaciones { get; set; }
    }
}
