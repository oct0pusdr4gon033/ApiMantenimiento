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
    public class ComprasController : ControllerBase
    {
        private readonly IComprasDocumentService _service;

        public ComprasController(IComprasDocumentService service)
        {
            _service = service;
        }

        // ==========================================
        // SOLICITUD DE PEDIDO (SOLPED)
        // ==========================================

        [HttpGet("solped")]
        public async Task<IActionResult> GetAllSolpeds()
        {
            var result = await _service.GetAllSolpedsAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("solped/{id}")]
        public async Task<IActionResult> GetSolpedById(int id)
        {
            try
            {
                var result = await _service.GetSolpedByIdAsync(id);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("solped")]
        public async Task<IActionResult> CrearSolicitudPedido([FromBody] SolicitudPedidoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CrearSolicitudPedidoAsync(request);
                return CreatedAtAction(nameof(GetSolpedById), new { id = result.id_solicitud_pedido }, new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("solped/{id}/aprobar")]
        public async Task<IActionResult> AprobarSolicitudPedido(int id)
        {
            try
            {
                var result = await _service.AprobarSolicitudPedidoAsync(id);
                return Ok(new { success = true, message = "Solicitud de Pedido aprobada y cotización generada con éxito.", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ==========================================
        // COTIZACIONES
        // ==========================================

        [HttpGet("cotizacion")]
        public async Task<IActionResult> GetAllCotizaciones()
        {
            var result = await _service.GetAllCotizacionesAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("cotizacion/{id}")]
        public async Task<IActionResult> GetCotizacionById(int id)
        {
            try
            {
                var result = await _service.GetCotizacionByIdAsync(id);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("cotizacion")]
        public async Task<IActionResult> CrearCotizacion([FromBody] CotizacionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CrearCotizacionAsync(request);
                return CreatedAtAction(nameof(GetCotizacionById), new { id = result.id_cotizacion }, new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("cotizacion/{id}")]
        public async Task<IActionResult> ActualizarCotizacion(int id, [FromBody] CotizacionUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.ActualizarCotizacionAsync(id, request);
                return Ok(new { success = true, message = "Cotización actualizada con éxito.", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("cotizacion/{id}/aprobar")]
        public async Task<IActionResult> AprobarCotizacion(int id)
        {
            try
            {
                var result = await _service.AprobarCotizacionAsync(id);
                return Ok(new { success = true, message = "Cotización aprobada y Orden de Compra generada con éxito.", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ==========================================
        // ORDEN DE COMPRA
        // ==========================================

        [HttpGet("orden-compra")]
        public async Task<IActionResult> GetAllOrdenesCompra()
        {
            var result = await _service.GetAllOrdenesCompraAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("orden-compra/{id}")]
        public async Task<IActionResult> GetOrdenCompraById(int id)
        {
            try
            {
                var result = await _service.GetOrdenCompraByIdAsync(id);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("orden-compra")]
        public async Task<IActionResult> CrearOrdenCompra([FromBody] OrdenCompraRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CrearOrdenCompraAsync(request);
                return CreatedAtAction(nameof(GetOrdenCompraById), new { id = result.id_orden_compra }, new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("orden-compra/{id}/aprobar")]
        public async Task<IActionResult> AprobarOrdenCompra(int id)
        {
            try
            {
                var result = await _service.AprobarOrdenCompraAsync(id);
                return Ok(new { success = true, message = "Orden de Compra aprobada con éxito.", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("orden-compra/{id}/recibir")]
        public async Task<IActionResult> ProcesarNotaIngreso(int id, [FromQuery] string observaciones)
        {
            try
            {
                var result = await _service.ProcesarNotaIngresoAsync(id, observaciones);
                return Ok(new { success = true, message = "Ingreso a almacén procesado con éxito. Stock de materiales incrementado.", data = result });
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ==========================================
        // NOTAS DE INGRESO
        // ==========================================

        [HttpGet("nota-ingreso")]
        public async Task<IActionResult> GetAllNotasIngreso()
        {
            var result = await _service.GetAllNotasIngresoAsync();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("nota-ingreso/{id}")]
        public async Task<IActionResult> GetNotaIngresoById(int id)
        {
            try
            {
                var result = await _service.GetNotaIngresoByIdAsync(id);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
