namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class ActividadSistemaResponse
    {
        public int id_actividad { get; set; }
        public string cod_act { get; set; }
        public int id_sistema { get; set; }
        public string nombre_actividad { get; set; }
        public string descripcion { get; set; }
        public int duracion { get; set; }
        public string medida_duracion { get; set; }
        public bool estado { get; set; }
    }
}
