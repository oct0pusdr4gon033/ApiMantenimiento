namespace ApiMantenimiento.Models.DTOS.MFlota
{
    /// <summary>
    /// Detalle completo de una Flota: incluye sus datos base
    /// más la lista de todos los Equipos asociados con su jerarquía completa.
    /// </summary>
    public class FlotaDetalleResponse
    {
        // ── Info de la Flota ────────────────────────────────
        public int IdFlota { get; set; }
        public string CodFlota { get; set; } = string.Empty;
        public string NombreFlota { get; set; } = string.Empty;
        public string TipoControl { get; set; } = string.Empty;

        // ── Modelo / Marca / Tipo (jerarquía de la flota) ──
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; } = string.Empty;
        public string NombreMarca { get; set; } = string.Empty;
        public string NombreTipo { get; set; } = string.Empty;

        // ── Equipos asociados ───────────────────────────────
        public int TotalEquipos { get; set; }
        public IEnumerable<EquipoResponse> Equipos { get; set; } = Enumerable.Empty<EquipoResponse>();
    }
}
