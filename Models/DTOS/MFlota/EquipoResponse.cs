namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class EquipoResponse
    {
        public int IdEquipo { get; set; }
        public string CodEqp { get; set; } = string.Empty;
        public string PlacaEqp { get; set; } = string.Empty;
        public string NumSerie { get; set; } = string.Empty;
        public decimal HorometroInicial { get; set; }
        public decimal HorometroActual { get; set; }
        public string EstadoOperativo { get; set; } = string.Empty;

        // Área de operación
        public string CodAreaOpe { get; set; } = string.Empty;
        public string NombreArea { get; set; } = string.Empty;

        // Flota
        public int IdFlota { get; set; }
        public string CodFlota { get; set; } = string.Empty;
        public string NombreFlota { get; set; } = string.Empty;

        // Modelo / Marca / Tipo (enriquecidos desde la jerarquía)
        public string NombreModelo { get; set; } = string.Empty;
        public string NombreMarca { get; set; } = string.Empty;
        public string NombreTipo { get; set; } = string.Empty;
    }
}
