using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MarcaEquipoController : ControllerBase
    {
        private readonly IMarcaEquipoService _service;

        public MarcaEquipoController(IMarcaEquipoService service)
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

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] MarcaEquipoRequest request)
        {
            var response = await _service.AgregarAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] MarcaEquipoRequest request)
        {
            var response = await _service.ActualizarAsync(id, request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
