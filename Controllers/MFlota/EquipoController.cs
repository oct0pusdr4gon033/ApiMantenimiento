using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoService _service;

        public EquipoController(IEquipoService service)
        {
            _service = service;
        }

        // ──────────────────────────────────────────────
        // CONSULTAS
        // ──────────────────────────────────────────────

        /// <summary>Retorna todos los equipos con su jerarquía completa.</summary>
        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var response = await _service.ListarAsync();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Busca un equipo por su ID interno.</summary>
        [HttpGet("buscar/id/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var response = await _service.BuscarPorIdAsync(id);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Busca un equipo por su código (cod_eqp).</summary>
        [HttpGet("buscar/codigo/{codigo}")]
        public async Task<IActionResult> BuscarPorCodigo(string codigo)
        {
            var response = await _service.BuscarPorCodigoAsync(codigo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Busca un equipo por su placa.</summary>
        [HttpGet("buscar/placa/{placa}")]
        public async Task<IActionResult> BuscarPorPlaca(string placa)
        {
            var response = await _service.BuscarPorPlacaAsync(placa);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Retorna todos los equipos asignados a un área de operación (ej: AREA-001).</summary>
        [HttpGet("buscar/area/{codArea}")]
        public async Task<IActionResult> BuscarPorArea(string codArea)
        {
            var response = await _service.BuscarPorAreaOperacionAsync(codArea);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Retorna todos los equipos de una flota por su código (ej: FL-001).</summary>
        [HttpGet("buscar/flota/{codFlota}")]
        public async Task<IActionResult> BuscarPorFlota(string codFlota)
        {
            var response = await _service.BuscarPorCodigoFlotaAsync(codFlota);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Retorna todos los equipos que pertenecen a un tipo de equipo.</summary>
        [HttpGet("buscar/tipo/{idTipoEqp}")]
        public async Task<IActionResult> BuscarPorTipoEquipo(int idTipoEqp)
        {
            var response = await _service.BuscarPorTipoEquipoAsync(idTipoEqp);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Retorna todos los equipos de una marca específica.</summary>
        [HttpGet("buscar/marca/{idMarca}")]
        public async Task<IActionResult> BuscarPorMarca(int idMarca)
        {
            var response = await _service.BuscarPorMarcaAsync(idMarca);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        /// <summary>Retorna todos los equipos de un modelo específico.</summary>
        [HttpGet("buscar/modelo/{idModelo}")]
        public async Task<IActionResult> BuscarPorModelo(int idModelo)
        {
            var response = await _service.BuscarPorModeloAsync(idModelo);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        // ──────────────────────────────────────────────
        // MUTACIONES
        // ──────────────────────────────────────────────

        /// <summary>
        /// Registra un nuevo equipo.
        /// EstadoOperativo válidos: OPERATIVO | MANTENIMIENTO | INACTIVO
        /// </summary>
        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] EquipoRequest request)
        {
            var response = await _service.AgregarAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        /// <summary>Actualiza los datos de un equipo existente por su ID.</summary>
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EquipoRequest request)
        {
            var response = await _service.ActualizarAsync(id, request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
