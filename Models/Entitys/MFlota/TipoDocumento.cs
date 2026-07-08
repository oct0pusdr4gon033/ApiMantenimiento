using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class TipoDocumento
    {
        public string cod_tipo_documento { get; set; }
        public string nombre_tipo { get; set; }
        
        // Relación 1 a N con ExpedienteDocumento
        public ICollection<ExpedienteDocumento> DetallesDocumento { get; set; }
    }
}
