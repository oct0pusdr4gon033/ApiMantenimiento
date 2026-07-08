namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class ExpedienteResponse
    {
        public string CodigoExp { get; set; } = string.Empty;

        // Datos del equipo vinculado
        public int IdEquipo { get; set; }
        public string CodEquipo { get; set; } = string.Empty;
        public string PlacaEquipo { get; set; } = string.Empty;

        // Detalle de documentos del expediente
        public IEnumerable<ExpedienteDocumentoResponse> Documentos { get; set; }
            = Enumerable.Empty<ExpedienteDocumentoResponse>();
    }
}
