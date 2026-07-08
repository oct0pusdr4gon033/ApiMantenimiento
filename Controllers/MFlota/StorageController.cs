using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMantenimiento.Controllers.MFlota
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] IFormFile file, 
            [FromForm] string modulo, 
            [FromForm] string version = "v1")
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, message = "Debe enviar un archivo." });

            if (string.IsNullOrWhiteSpace(modulo))
                return BadRequest(new { success = false, message = "El módulo es requerido." });

            var response = await _storageService.SubirArchivoAsync(file, version, modulo);
            
            if (response.Success)
                return Ok(response);
            
            return BadRequest(response);
        }
    }
}
