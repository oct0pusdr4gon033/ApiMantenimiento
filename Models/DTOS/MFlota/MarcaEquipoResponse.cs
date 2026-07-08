namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class MarcaEquipoResponse
    {
        // Aquí SÍ necesitamos el ID, porque no tenemos un código de texto para identificarla
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; } = string.Empty;
    }
}
