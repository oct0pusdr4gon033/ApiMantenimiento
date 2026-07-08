using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MFlota
{
    public class EquipoRequest
    {
        [Required(ErrorMessage = "El ID de flota es obligatorio.")]
        public int IdFlota { get; set; }

        [Required(ErrorMessage = "El código del equipo es obligatorio.")]
        [StringLength(18, ErrorMessage = "El código no puede exceder los 18 caracteres.")]
        public string CodEqp { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "La placa no puede exceder los 10 caracteres.")]
        public string PlacaEqp { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El número de serie no puede exceder los 100 caracteres.")]
        public string NumSerie { get; set; } = string.Empty;

        [Required(ErrorMessage = "El horómetro inicial es obligatorio.")]
        [Range(0, 9999999.99, ErrorMessage = "El horómetro inicial debe ser un valor positivo.")]
        public decimal HorometroInicial { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "El horómetro actual debe ser un valor positivo.")]
        public decimal HorometroActual { get; set; }

        /// <summary>Valores válidos: OPERATIVO | MANTENIMIENTO | INACTIVO</summary>
        [Required(ErrorMessage = "El estado operativo es obligatorio.")]
        public string EstadoOperativo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de área de operación es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código de área no puede exceder los 10 caracteres.")]
        public string CodAreaOpe { get; set; } = string.Empty;
    }
}
