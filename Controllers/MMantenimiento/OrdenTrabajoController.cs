using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiMantenimiento.Controllers.MMantenimiento
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdenTrabajoController : ControllerBase
    {
        private readonly IOrdenTrabajoService _service;

        public OrdenTrabajoController(IOrdenTrabajoService service)
        {
            _service = service;
        }

        // ── OTs ──────────────────────────────────────────────────

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("equipo/{idEquipo}")]
        public async Task<IActionResult> GetByEquipo(int idEquipo)
        {
            var result = await _service.GetByEquipoAsync(idEquipo);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{idOt}")]
        public async Task<IActionResult> GetById(int idOt)
        {
            var result = await _service.GetByIdAsync(idOt);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrdenTrabajoCreateRequest request)
        {
            var result = await _service.CreateManualAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{idOt}/estado")]
        public async Task<IActionResult> CambiarEstado(int idOt, [FromBody] CambiarEstadoOTRequest request)
        {
            var result = await _service.CambiarEstadoAsync(idOt, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── Calendario ───────────────────────────────────────────

        [HttpGet("calendario/equipo/{idEquipo}")]
        public async Task<IActionResult> GetCalendarioEquipo(int idEquipo)
        {
            var result = await _service.GetCalendarioAsync(idEquipo);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("calendario/flota/{idFlota}")]
        public async Task<IActionResult> GetCalendarioFlota(int idFlota)
        {
            var result = await _service.GetCalendarioFlotaAsync(idFlota);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── Configuración ─────────────────────────────────────────

        [HttpGet("configuracion")]
        public async Task<IActionResult> GetConfiguracion([FromQuery] int? idFlota)
        {
            var result = await _service.GetConfiguracionFlotaAsync(idFlota);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("configuracion")]
        public async Task<IActionResult> SetConfiguracion([FromBody] ConfiguracionFlotaRequest request)
        {
            var result = await _service.SetConfiguracionFlotaAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── Actividades y Materiales Extra ───────────────────────

        [HttpPost("{idOt}/actividad")]
        public async Task<IActionResult> AddActividadExtra(int idOt, [FromBody] AgregarActividadOTRequest request)
        {
            var result = await _service.AddActividadExtraAsync(idOt, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("actividad/{idOtActividad}")]
        public async Task<IActionResult> RemoveActividadExtra(int idOtActividad)
        {
            var result = await _service.RemoveActividadExtraAsync(idOtActividad);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{idOt}/material")]
        public async Task<IActionResult> AddMaterialExtra(int idOt, [FromBody] AgregarMaterialOTRequest request)
        {
            var result = await _service.AddMaterialExtraAsync(idOt, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("material/{idOtMaterial}")]
        public async Task<IActionResult> RemoveMaterialExtra(int idOtMaterial)
        {
            var result = await _service.RemoveMaterialExtraAsync(idOtMaterial);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── Personal Extra ───────────────────────────────────────

        [HttpPost("{idOt}/personal")]
        public async Task<IActionResult> AddPersonalExtra(int idOt, [FromBody] AgregarPersonalOTRequest request)
        {
            var result = await _service.AddPersonalExtraAsync(idOt, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("personal/{idOtPersonal}")]
        public async Task<IActionResult> RemovePersonalExtra(int idOtPersonal)
        {
            var result = await _service.RemovePersonalExtraAsync(idOtPersonal);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{idOt}/tecnicos-disponibles")]
        public async Task<IActionResult> GetTecnicosDisponibles(int idOt)
        {
            var result = await _service.GetTecnicosDisponiblesAsync(idOt);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
