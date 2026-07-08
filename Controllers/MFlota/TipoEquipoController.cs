using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TipoEquipoController : ControllerBase
    {
        private readonly ITipoEquipoService _service;

        public TipoEquipoController(ITipoEquipoService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var response = await _service.ListarAsync();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("buscar/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var response = await _service.BuscarPorIdAsync(id);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        [HttpGet("buscar/filtro/{texto}")]
        public async Task<IActionResult> BuscarPorFiltro(string texto)
        {
            var response = await _service.BuscarPorFiltroAsync(texto);
            if (response.Success) return Ok(response);
            return NotFound(response);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] TipoEquipoRequest request)
        {
            var response = await _service.AgregarAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] TipoEquipoRequest request)
        {
            var response = await _service.ActualizarAsync(id, request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
