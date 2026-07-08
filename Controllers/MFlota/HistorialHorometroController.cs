using ApiMantenimiento.Models.Requests.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HistorialHorometroController : ControllerBase
    {
        private readonly IHistorialHorometroService _historialService;

        public HistorialHorometroController(IHistorialHorometroService historialService)
        {
            _historialService = historialService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var response = await _historialService.ObtenerTodosAsync();
            return Ok(response);
        }

        [HttpGet("equipo/{idEquipo}")]
        public async Task<IActionResult> ObtenerPorEquipo(int idEquipo)
        {
            var response = await _historialService.ObtenerPorEquipoAsync(idEquipo);
            return Ok(response);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            var response = await _historialService.ObtenerPorCodigoAsync(codigo);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] HistorialHorometroRequest request)
        {
            var response = await _historialService.CrearAsync(request);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
