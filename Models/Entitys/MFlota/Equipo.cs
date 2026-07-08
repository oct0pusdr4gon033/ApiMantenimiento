namespace ApiMantenimiento.Models.Entitys.MFlota
{
    public class Equipo
    {
        public int id_equipo { get; set; }
        public int id_flota { get; set; }
        public string cod_eqp { get; set; }
        public string placa_eqp { get; set; }
        public string num_serie { get; set; }
        public decimal horometro_inicial { get; set; }
        public decimal horometro_actual { get; set; }
        public string estado_operativo { get; set; }
        public string cod_are_ope { get; set; }
        
        public Flota Flota { get; set; }
        public AreaOperacion AreaOperacion { get; set; }
        
        // Relación 1 a 1 con Expediente
        public Expediente Expediente { get; set; }
    }
}
