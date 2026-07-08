using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiMantenimiento.Controllers.MMantenimiento
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanMantenimientoController : ControllerBase
    {
        private readonly IPlanMantenimientoService _planService;

        public PlanMantenimientoController(IPlanMantenimientoService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _planService.GetAllAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _planService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanMantenimientoRequest request)
        {
            var result = await _planService.CreateAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlanMantenimientoUpdateRequest request)
        {
            var result = await _planService.UpdateAsync(id, request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _planService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // Endpoints específicos para Actividades
        [HttpPost("{id}/actividades")]
        public async Task<IActionResult> AddActividad(int id, [FromBody] PlanMantenimientoActividadRequest request)
        {
            var result = await _planService.AddActividadAsync(id, request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id_plan_mant}/actividades/{id_actividad}/{id_detalle_estrg}")]
        public async Task<IActionResult> RemoveActividad(int id_plan_mant, int id_actividad, int id_detalle_estrg)
        {
            var result = await _planService.RemoveActividadAsync(id_plan_mant, id_actividad, id_detalle_estrg);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }


        // Endpoints específicos para Personal
        [HttpPost("{id}/personal")]
        public async Task<IActionResult> AddPersonal(int id, [FromBody] PlanMantenimientoPersonalRequest request)
        {
            var result = await _planService.AddPersonalAsync(id, request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("personal/{id_plan_personal}")]
        public async Task<IActionResult> RemovePersonal(int id_plan_personal)
        {
            var result = await _planService.RemovePersonalAsync(id_plan_personal);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
