using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Services.Interfaces.MEmpleados;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MEmpleados
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerActivos()
        {
            return Ok(await _empleadoService.ObtenerTodosActivosAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _empleadoService.ObtenerTodosAsync());
        }

        [HttpGet("{dni}")]
        public async Task<IActionResult> ObtenerPorDni(string dni)
        {
            var response = await _empleadoService.ObtenerPorDniAsync(dni);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] EmpleadoRequest request)
        {
            var response = await _empleadoService.CrearAsync(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("{dni}")]
        public async Task<IActionResult> Actualizar(string dni, [FromBody] EmpleadoRequest request)
        {
            var response = await _empleadoService.ActualizarAsync(dni, request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{dni}")]
        public async Task<IActionResult> Eliminar(string dni)
        {
            var response = await _empleadoService.EliminarLogicoAsync(dni);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}
