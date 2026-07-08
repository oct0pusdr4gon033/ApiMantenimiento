namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class EstrategiaDetalle
    {
        public int id_detalle_estrg { get; set; }
        public int id_estrategia { get; set; }
        public decimal umbral_mant { get; set; }
        public decimal tolerancia_inf { get; set; }
        public decimal tolerancia_sup { get; set; }
        public decimal porcentaje_tol { get; set; }
        public string nombre_medida { get; set; }///puede ser solo horometros o km ,dias,meses
        public string uni_med { get; set; } //H,KM,D,M
        public string tipo_pm { get; set; } // Ej: PM1, PM2, etc.

        public Estrategia Estrategia { get; set; }
    }
}
