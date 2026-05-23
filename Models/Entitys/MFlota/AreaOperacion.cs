using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.Entitys.MFlota
{
    
    public class AreaOperacion
    {
        
        public string cod_area_ope { get; set; }
        public string nomb_area { get; set; }

        public ICollection<Equipo> Equipos { get; set; }
    }
}
