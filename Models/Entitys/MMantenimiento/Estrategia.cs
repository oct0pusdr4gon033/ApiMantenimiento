using System.Collections.Generic;
using ApiMantenimiento.Models.Entitys.MFlota;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class Estrategia
    {
        public int id_estrategia { get; set;  }
        public string cod_estrategia { get; set; }
        public string titulo_estrategia { get; set; } 
        public int? id_flota { get; set; } // Opcional, si es para una flota
        public int? id_equipo { get; set; } // Opcional, si es para un equipo
        public string estado { get; set; }

        public Flota Flota { get; set; }
        public Equipo Equipo { get; set; }
        
        public ICollection<EstrategiaDetalle> Detalles { get; set; }
    }
}
