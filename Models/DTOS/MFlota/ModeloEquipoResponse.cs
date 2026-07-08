namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class ModeloEquipoResponse
    {
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; } = string.Empty;
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; } = string.Empty;
        public int IdTipoEqp { get; set; }
        public string NombreTipo { get; set; } = string.Empty;
    }
}
