namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class ActividadSistema
    {
        public int id_actividad { get; set; }
        public string cod_act { get; set; }
        
        // Foreign Key
        public int id_sistema { get; set; }
        public SistemaEquipo SistemaEquipo { get; set; }

        public string nombre_actividad { get; set; }
        public string descripcion { get; set; }
        public int duracion { get; set; } // Representa el valor numérico
        public string medida_duracion { get; set; } // "H" o "MIN"
        public bool estado { get; set; }
    }
}
