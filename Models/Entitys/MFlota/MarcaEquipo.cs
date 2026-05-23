namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class MarcaEquipo
    {
        public int id_marca { get; set;  }
        public string nombre_marca { get; set;}

        // Propiedad de navegación: Una marca tiene MUCHOS modelos
        public ICollection<ModeloEquipo> Modelos { get; set; }
    }
}
