using System.Diagnostics.Contracts;

namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class Flota
    {
        public int id_flota { get; set; }
        public string cod_flota {get;set;} 
        public int id_modelo { get; set; }
        public string nombre_flota {get; set; }
        public string tipo_control { get; set;}

        public ModeloEquipo ModeloEquipo { get; set; }

        // Propiedad de navegación: Una flota agrupa MUCHOS equipos
        public ICollection<Equipo> Equipos { get; set; }
    }
}
