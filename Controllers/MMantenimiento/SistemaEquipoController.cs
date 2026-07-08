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
    public class SistemaEquipoController : ControllerBase
    {
        private readonly ISistemaEquipoService _service;

        public SistemaEquipoController(ISistemaEquipoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SistemaEquipoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetAll), new { id = result.id_sistema }, new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SistemaEquipoUpdateRequest request)
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

        [HttpGet("{id}/subsistemas")]
        public async Task<IActionResult> GetSubSistemas(int id)
        {
            var result = await _service.GetSubSistemasBySistemaAsync(id);
            return Ok(new { success = true, data = result });
        }

        [HttpPost("subsistemas")]
        public async Task<IActionResult> CreateSubSistema([FromBody] SubSistemaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateSubSistemaAsync(request);
                return Ok(new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
