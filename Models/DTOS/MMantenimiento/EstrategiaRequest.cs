using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    public class EstrategiaRequest
    {
        public string cod_estrategia { get; set; }
        public string titulo_estrategia { get; set; } 
        public int? id_flota { get; set; }
        public int? id_equipo { get; set; }
        public string estado { get; set; }

        public List<EstrategiaDetalleRequest> Detalles { get; set; } = new List<EstrategiaDetalleRequest>();
    }

    public class EstrategiaDetalleRequest
    {
        public decimal umbral_mant { get; set; }
        public decimal tolerancia_inf { get; set; }
        public decimal tolerancia_sup { get; set; }
        public decimal porcentaje_tol { get; set; }
        public string nombre_medida { get; set; }
        public string uni_med { get; set; }
        public string tipo_pm { get; set; }
    }
}
