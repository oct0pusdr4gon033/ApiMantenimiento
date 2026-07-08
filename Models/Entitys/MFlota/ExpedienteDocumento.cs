namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class ExpedienteDocumento
    {
        public int id_expediente_documento { get; set; }
        public string codigo_exp { get; set; }
        public string cod_tipo_documento { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public string documento_url { get; set; }


        // Relación con Expediente
        public Expediente Expediente { get; set; }
        
        // Relación con TipoDocumento
        public TipoDocumento TipoDocumento { get; set; }
    }
}
