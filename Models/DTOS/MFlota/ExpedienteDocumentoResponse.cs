namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class ExpedienteDocumentoResponse
    {
        public int IdExpedienteDocumento { get; set; }

        // Expediente padre
        public string CodigoExp { get; set; } = string.Empty;

        // Tipo de documento
        public string CodTipoDocumento { get; set; } = string.Empty;
        public string NombreTipoDocumento { get; set; } = string.Empty;

        // Datos del documento
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string DocumentoUrl { get; set; } = string.Empty;

        /// <summary>Indica si el documento ya venció.</summary>
        public bool EstaVencido => FechaVencimiento < DateTime.UtcNow;
    }
}
