using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;

namespace ApiMantenimiento.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EstrategiaController : ControllerBase
    {
        private readonly IEstrategiaService _estrategiaService;

        public EstrategiaController(IEstrategiaService estrategiaService)
        {
            _estrategiaService = estrategiaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstrategiaResponse>>> GetAll()
        {
            var estrategias = await _estrategiaService.GetAllEstrategiasAsync();
            return Ok(estrategias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstrategiaResponse>> GetById(int id)
        {
            var estrategia = await _estrategiaService.GetEstrategiaByIdAsync(id);
            if (estrategia == null)
            {
                return NotFound();
            }
            return Ok(estrategia);
        }

        [HttpPost]
        public async Task<ActionResult<EstrategiaResponse>> Create([FromBody] EstrategiaRequest request)
        {
            try
            {
                var result = await _estrategiaService.CreateEstrategiaAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = result.id_estrategia }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al crear la estrategia: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EstrategiaResponse>> Update(int id, [FromBody] EstrategiaUpdateRequest request)
        {
            try
            {
                var result = await _estrategiaService.UpdateEstrategiaAsync(id, request);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al actualizar la estrategia: {ex.Message}");
            }
        }
    }
}
