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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _service;

        public MaterialController(IMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new { success = true, data = result });
        }

        // Advanced Search Endpoint
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(
            [FromQuery] string cod_materia, 
            [FromQuery] string nombre, 
            [FromQuery] string estado, 
            [FromQuery] int? id_unidad, 
            [FromQuery] int? id_categoria)
        {
            var result = await _service.BuscarMaterialesAsync(cod_materia, nombre, estado, id_unidad, id_categoria);
            return Ok(new { success = true, data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaterialRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetAll), new { id = result.id_material }, new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MaterialUpdateRequest request)
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

        [HttpPost("{id}/entrada")]
        public async Task<IActionResult> RegistrarEntradaStock(int id, [FromBody] StockInflowRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.RegistrarEntradaStockAsync(id, request);
                return Ok(new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}/kardex")]
        public async Task<IActionResult> GetKardex(int id)
        {
            try
            {
                var result = await _service.GetKardexAsync(id);
                return Ok(new { success = true, data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
