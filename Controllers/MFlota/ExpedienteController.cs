using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpedienteController : ControllerBase
    {
        private readonly IExpedienteService _service;

        public ExpedienteController(IExpedienteService service)
        {
            _service = service;
        }

        // ──────────────────────────────────────────────
        // CONSULTAS
        // ──────────────────────────────────────────────

        /// <summary>Retorna todos los expedientes con su detalle de documentos.</summary>
        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var response = await _service.ListarAsync();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Busca un expediente por su código (ej: EXP-001).</summary>
        [HttpGet("buscar/codigo/{codigo}")]
        public async Task<IActionResult> BuscarPorCodigo(string codigo)
        {
            var response = await _service.BuscarPorCodigoAsync(codigo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Busca el expediente asociado a un equipo por su ID.</summary>
        [HttpGet("buscar/equipo/{idEquipo:int}")]
        public async Task<IActionResult> BuscarPorEquipo(int idEquipo)
        {
            var response = await _service.BuscarPorEquipoAsync(idEquipo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        // ──────────────────────────────────────────────
        // MUTACIONES
        // ──────────────────────────────────────────────

        /// <summary>
        /// Crea un nuevo expediente vinculado a un equipo.
        /// Cada equipo solo puede tener un expediente (relación 1 a 1).
        /// </summary>
        [HttpPost("crear")]
        public async Task<IActionResult> CrearExpediente([FromBody] ExpedienteRequest request)
        {
            var response = await _service.CrearExpedienteAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Inserta un documento al detalle del expediente especificado.
        /// Valida que el expediente y el tipo de documento existan.
        /// </summary>
        [HttpPost("insertar-documento")]
        public async Task<IActionResult> InsertarDocumento([FromBody] ExpedienteDocumentoRequest request)
        {
            var response = await _service.InsertarDocumentoAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Obtiene el detalle de un documento del expediente por su ID.</summary>
        [HttpGet("documento/{id:int}")]
        public async Task<IActionResult> ObtenerDocumentoPorId(int id)
        {
            var response = await _service.ObtenerDocumentoPorIdAsync(id);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Actualiza la información de un documento.</summary>
        [HttpPut("documento/{id:int}")]
        public async Task<IActionResult> ActualizarDocumento(int id, [FromBody] ExpedienteDocumentoRequest request)
        {
            var response = await _service.ActualizarDocumentoAsync(id, request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
