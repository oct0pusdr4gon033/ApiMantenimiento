namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class TipoEquipo
    {
        public int id_tipo_eqp { get; set; }
        public string cod_equipo { get; set; }
        public string nombre_tipo { get; set; } 
        public ICollection<ModeloEquipo> ModeloEquipos { get; set; } 
    }
}
