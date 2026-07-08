using ApiMantenimiento.Models.DTOS.MCompras;
using ApiMantenimiento.Services.Interfaces.MCompras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiMantenimiento.Controllers.MCompras
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _service;

        public ProveedorController(IProveedorService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var result = await _service.GetAllAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("{ruc}")]
        public async Task<IActionResult> GetByRuc(string ruc)
        {
            try
            {
                var result = await _service.GetByRucAsync(ruc);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(
            [FromQuery] string? ruc, 
            [FromQuery] string? razonSocial, 
            [FromQuery] string? codCat)
        {
            var result = await _service.BuscarProveedoresAsync(ruc, razonSocial, codCat);
            return Ok(new { success = true, data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProveedorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetByRuc), new { ruc = result.ruc }, new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{ruc}")]
        public async Task<IActionResult> Update(string ruc, [FromBody] ProveedorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(ruc, request);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("{ruc}/contacto")]
        public async Task<IActionResult> AddContacto(string ruc, [FromBody] ProveedorContactoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.AddContactoAsync(ruc, request);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("contacto/{idContacto}")]
        public async Task<IActionResult> UpdateContacto(int idContacto, [FromBody] ProveedorContactoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateContactoAsync(idContacto, request);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var result = await _service.GetCategoriasAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpPost("categorias")]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaProveedorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateCategoriaAsync(request);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
