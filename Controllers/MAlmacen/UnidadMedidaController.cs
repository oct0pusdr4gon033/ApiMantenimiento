using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Services.Interfaces.MAlmacen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiMantenimiento.Controllers.MAlmacen
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IUnidadMedidaService _service;

        public UnidadMedidaController(IUnidadMedidaService service)
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
        public async Task<IActionResult> Create([FromBody] UnidadMedidaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetAll), new { id = result.id_unidad }, new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UnidadMedidaRequest request)
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
