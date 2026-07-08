using System.Collections.Generic;
using System.Linq;

namespace ApiMantenimiento.Models.DTOS.MEmpleados
{
    public class ExpedienteEmpleadoResponse
    {
        public string CodigoExpEmp { get; set; } = string.Empty;

        // Datos del empleado vinculado
        public string DniEmpleado { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = string.Empty;

        // Detalle de documentos del expediente
        public IEnumerable<ExpedienteDocumentoEmpleadoResponse> Documentos { get; set; }
            = Enumerable.Empty<ExpedienteDocumentoEmpleadoResponse>();
    }
}
