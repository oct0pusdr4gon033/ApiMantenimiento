using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota; // Tu interfaz corregida
using Microsoft.AspNetCore.Mvc;
namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AreaOperacionController : ControllerBase
    {
        private readonly IAreaOperacionService _service;
        public AreaOperacionController(IAreaOperacionService service)
        {
            _service = service;
        }
        [HttpGet("listar")]
        public async Task<IActionResult> ListarAreas()
        {
            var response = await _service.ListarAreasAsync();
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("buscar/codigo/{codigoArea}")]
        public async Task<IActionResult> BuscarPorCodigo(string codigoArea)
        {
            var response = await _service.BuscarPorCodigoAsync(codigoArea);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }
        [HttpGet("buscar/nombre/{nombreArea}")]
        public async Task<IActionResult> BuscarPorNombre(string nombreArea)
        {
            var response = await _service.BuscarPorNombreAsync(nombreArea);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }
        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarArea([FromBody] AreaOperacionRequest request)
        {
            var response = await _service.AgregarAreaAsync(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);

        }
    }
}
