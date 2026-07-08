using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Services.Interfaces.MEmpleados;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MEmpleados
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _rolService.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var response = await _rolService.ObtenerPorIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RolRequest request)
        {
            var response = await _rolService.CrearAsync(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] RolRequest request)
        {
            var response = await _rolService.ActualizarAsync(id, request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
