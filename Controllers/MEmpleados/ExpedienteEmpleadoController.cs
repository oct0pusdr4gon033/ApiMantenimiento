using System;
using System.Threading.Tasks;
using ApiMantenimiento.Models.DTOS.MEmpleados;
using ApiMantenimiento.Services.Interfaces.MEmpleados;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MEmpleados
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteEmpleadoController : ControllerBase
    {
        private readonly IExpedienteEmpleadoService _expedienteService;

        public ExpedienteEmpleadoController(IExpedienteEmpleadoService expedienteService)
        {
            _expedienteService = expedienteService;
        }

        // --- EXPEDIENTES ---

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var expedientes = await _expedienteService.ObtenerTodosAsync();
                return Ok(expedientes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            try
            {
                var expediente = await _expedienteService.ObtenerPorCodigoAsync(codigo);
                return Ok(expediente);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("dni/{dni}")]
        public async Task<IActionResult> ObtenerPorDni(string dni)
        {
            try
            {
                var expediente = await _expedienteService.ObtenerPorDniAsync(dni);
                return Ok(expediente);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearExpediente([FromBody] ExpedienteEmpleadoRequest request)
        {
            try
            {
                var expediente = await _expedienteService.CrearExpedienteAsync(request);
                return CreatedAtAction(nameof(ObtenerPorCodigo), new { codigo = expediente.CodigoExpEmp }, expediente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // --- TIPOS DE DOCUMENTO ---

        [HttpGet("tipos")]
        public async Task<IActionResult> ObtenerTiposDocumento()
        {
            try
            {
                var tipos = await _expedienteService.ObtenerTiposDocumentoAsync();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("tipos")]
        public async Task<IActionResult> CrearTipoDocumento([FromBody] TipoDocumentoEmpleadoRequest request)
        {
            try
            {
                var tipo = await _expedienteService.CrearTipoDocumentoAsync(request);
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // --- DETALLES DE DOCUMENTO ---

        [HttpGet("{codigoExp}/documentos")]
        public async Task<IActionResult> ObtenerDocumentos(string codigoExp)
        {
            try
            {
                var documentos = await _expedienteService.ObtenerDocumentosAsync(codigoExp);
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("documentos")]
        public async Task<IActionResult> AgregarDocumento([FromBody] ExpedienteDocumentoEmpleadoRequest request)
        {
            try
            {
                var doc = await _expedienteService.AgregarDocumentoAsync(request);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("documentos/{id}")]
        public async Task<IActionResult> ActualizarDocumento(int id, [FromBody] ExpedienteDocumentoEmpleadoRequest request)
        {
            try
            {
                var doc = await _expedienteService.ActualizarDocumentoAsync(id, request);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("documentos/{id}")]
        public async Task<IActionResult> EliminarDocumento(int id)
        {
            try
            {
                await _expedienteService.EliminarDocumentoAsync(id);
                return Ok(new { Message = "Documento eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
