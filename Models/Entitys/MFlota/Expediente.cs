using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class Expediente
    {
        public string codigo_exp { get; set; }
        public int id_equipo { get; set; }

        // Relación 1 a 1 con Equipo
        public Equipo Equipo { get; set; }

        // Relación 1 a N con ExpedienteDocumento
        public ICollection<ExpedienteDocumento> DetallesDocumento { get; set; }
    }
}
