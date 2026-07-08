using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MEmpleados
{
    public class TipoDocumentoEmpleado
    {
        public string cod_tipo_doc_emp { get; set; }
        public string nombre_tipo { get; set; }
        
        // Relación 1 a N con ExpedienteDocumentoEmpleado
        public ICollection<ExpedienteDocumentoEmpleado> DetallesDocumento { get; set; }
    }
}
