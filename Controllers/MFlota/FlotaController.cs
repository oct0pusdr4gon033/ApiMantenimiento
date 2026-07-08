using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FlotaController : ControllerBase
    {
        private readonly IFlotaService _service;

        public FlotaController(IFlotaService service)
        {
            _service = service;
        }

        // ──────────────────────────────────────────────────────
        // LISTADO Y CONSULTAS
        // ──────────────────────────────────────────────────────

        /// <summary>Retorna todas las flotas con su modelo, marca y tipo de equipo.</summary>
        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var response = await _service.ListarAsync();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Busca una flota por su ID interno.</summary>
        [HttpGet("buscar/id/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var response = await _service.BuscarPorIdAsync(id);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Busca una flota por su código único (ej: FL-001).</summary>
        [HttpGet("buscar/codigo/{codFlota}")]
        public async Task<IActionResult> BuscarPorCodigo(string codFlota)
        {
            var response = await _service.BuscarPorCodigoAsync(codFlota);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>
        /// Busca flotas por tipo de equipo (búsqueda parcial).
        /// Ej: "CAMION" trae CAMION CISTERNA, CAMION VOLQUETE, etc.
        /// </summary>
        [HttpGet("buscar/tipo/{nombreTipo}")]
        public async Task<IActionResult> BuscarPorTipoEquipo(string nombreTipo)
        {
            var response = await _service.BuscarPorTipoEquipoAsync(nombreTipo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Busca flotas cuyo modelo contenga el texto (búsqueda parcial).</summary>
        [HttpGet("buscar/modelo/{nombreModelo}")]
        public async Task<IActionResult> BuscarPorModelo(string nombreModelo)
        {
            var response = await _service.BuscarPorModeloAsync(nombreModelo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        // ──────────────────────────────────────────────────────
        // DETALLE DE FLOTA
        // ──────────────────────────────────────────────────────

        /// <summary>
        /// Retorna el detalle completo de una flota identificada por su código:
        /// datos de la flota + todos sus equipos con su jerarquía completa
        /// (Marca, Modelo, TipoEquipo, AreaOperacion, horómetros, estado operativo, etc.).
        /// </summary>
        [HttpGet("detalle/{codFlota}")]
        public async Task<IActionResult> ObtenerDetalle(string codFlota)
        {
            var response = await _service.ObtenerDetalleAsync(codFlota);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        // ──────────────────────────────────────────────────────
        // MUTACIONES
        // ──────────────────────────────────────────────────────

        /// <summary>Registra una nueva flota. El CodFlota debe ser único en el sistema.</summary>
        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] FlotaRequest request)
        {
            var response = await _service.AgregarAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Actualiza los datos de una flota existente por su ID.</summary>
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] FlotaRequest request)
        {
            var response = await _service.ActualizarAsync(id, request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
