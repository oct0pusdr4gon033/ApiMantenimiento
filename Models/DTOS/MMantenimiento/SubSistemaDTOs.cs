namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class SubSistemaResponse
    {
        public int id_subsistema { get; set; }
        public string cod_subsist { get; set; }
        public string nombre_subsist { get; set; }
        public int id_sistema { get; set; }
    }

    public class SubSistemaRequest
    {
        public string cod_subsist { get; set; }
        public string nombre_subsist { get; set; }
        public int id_sistema { get; set; }
    }
}
