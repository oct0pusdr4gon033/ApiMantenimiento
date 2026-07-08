using System.ComponentModel.DataAnnotations;

namespace ApiMantenimiento.Models.DTOS.MEmpleados
{
    public class ExpedienteEmpleadoRequest
    {
        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string CodigoExpEmp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI del empleado es obligatorio.")]
        [StringLength(20, ErrorMessage = "El DNI no puede exceder los 20 caracteres.")]
        public string DniEmpleado { get; set; } = string.Empty;
    }
}
