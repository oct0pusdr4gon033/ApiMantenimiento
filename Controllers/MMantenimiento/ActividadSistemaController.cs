using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiMantenimiento.Controllers.MMantenimiento
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ActividadSistemaController : ControllerBase
    {
        private readonly IActividadSistemaService _service;

        public ActividadSistemaController(IActividadSistemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorSistemaONombre([FromQuery] string termino)
        {
            var result = await _service.BuscarPorSistemaONombreAsync(termino);
            return Ok(new { success = true, data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActividadSistemaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetAll), new { id = result.id_actividad }, new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActividadSistemaUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(id, request);
                return Ok(new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
