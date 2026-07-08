using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _service;

        public TipoDocumentoController(ITipoDocumentoService service)
        {
            _service = service;
        }

        // ──────────────────────────────────────────────
        // CONSULTAS
        // ──────────────────────────────────────────────

        /// <summary>Retorna todos los tipos de documento registrados.</summary>
        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var response = await _service.ListarAsync();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Busca un tipo de documento por su código (ej: DOC-001).</summary>
        [HttpGet("buscar/{codigo}")]
        public async Task<IActionResult> BuscarPorCodigo(string codigo)
        {
            var response = await _service.BuscarPorCodigoAsync(codigo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        // ──────────────────────────────────────────────
        // MUTACIONES
        // ──────────────────────────────────────────────

        /// <summary>Registra un nuevo tipo de documento.</summary>
        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] TipoDocumentoRequest request)
        {
            var response = await _service.AgregarAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Actualiza los datos de un tipo de documento existente.
        /// No se permite eliminar para mantener la integridad referencial.
        /// </summary>
        [HttpPut("actualizar/{codigo}")]
        public async Task<IActionResult> Actualizar(string codigo, [FromBody] TipoDocumentoRequest request)
        {
            var response = await _service.ActualizarAsync(codigo, request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
