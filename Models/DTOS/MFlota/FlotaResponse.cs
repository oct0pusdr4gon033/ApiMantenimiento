namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class FlotaResponse
    {
        public int IdFlota { get; set; }
        public string CodFlota { get; set; } = string.Empty;
        public string NombreFlota { get; set; } = string.Empty;
        public string TipoControl { get; set; } = string.Empty;
        // Datos enriquecidos del Modelo
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; } = string.Empty;
        public string NombreMarca { get; set; } = string.Empty;
        public string NombreTipo { get; set; } = string.Empty;
    }
}
