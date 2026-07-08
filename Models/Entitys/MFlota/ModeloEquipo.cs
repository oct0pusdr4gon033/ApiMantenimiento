namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class ModeloEquipo
    {
        public int id_modelo { get; set; }
        public int id_marca { get; set; }
        public int id_tipo_eqp { get; set; }
        public string nomb_modelo { get; set; }
        public MarcaEquipo Marca { get; set; }
        public TipoEquipo TipoEquipo { get; set; }

        public ICollection<Flota> Flotas { get; set; }
    }
}
