using System;

namespace ApiMantenimiento.Models.Entitys.MEmpleados
{
    public class ExpedienteDocumentoEmpleado
    {
        public int id_exp_doc_emp { get; set; }
        public string codigo_exp_emp { get; set; }
        public string cod_tipo_doc_emp { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public string documento_url { get; set; }

        // Relación con ExpedienteEmpleado
        public ExpedienteEmpleado ExpedienteEmpleado { get; set; }
        
        // Relación con TipoDocumentoEmpleado
        public TipoDocumentoEmpleado TipoDocumentoEmpleado { get; set; }
    }
}
