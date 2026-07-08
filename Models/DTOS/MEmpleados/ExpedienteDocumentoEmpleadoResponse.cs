using System;

namespace ApiMantenimiento.Models.DTOS.MEmpleados
{
    public class ExpedienteDocumentoEmpleadoResponse
    {
        public int IdExpedienteDocumentoEmp { get; set; }

        // Expediente padre
        public string CodigoExpEmp { get; set; } = string.Empty;

        // Tipo de documento
        public string CodTipoDocumentoEmp { get; set; } = string.Empty;
        public string NombreTipoDocumento { get; set; } = string.Empty;

        // Datos del documento
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string DocumentoUrl { get; set; } = string.Empty;

        /// <summary>Indica si el documento ya venció.</summary>
        public bool EstaVencido => FechaVencimiento.HasValue && FechaVencimiento.Value < DateTime.UtcNow;
    }
}
