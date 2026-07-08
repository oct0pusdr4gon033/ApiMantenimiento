using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Services.Interfaces.MFlota;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ApiMantenimiento.Services.MFlota
{
    public class LocalStorageService : IStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> SubirArchivoAsync(IFormFile archivo, string version, string modulo)
        {
            if (archivo == null || archivo.Length == 0)
                return ApiResponse<string>.Fail("No se ha enviado ningún archivo.");

            // Validar si wwwroot está definido, en caso de que no exista
            string webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // Ruta base: wwwroot/uploads/{version}/{modulo}
            string rutaRelativaCarpeta = Path.Combine("uploads", version, modulo).Replace("\\", "/");
            string rutaCarpeta = Path.Combine(webRoot, "uploads", version, modulo);

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            // Generar nombre de archivo único
            string extension = Path.GetExtension(archivo.FileName);
            string nombreUnico = $"{Guid.NewGuid()}{extension}";
            string rutaCompletaArchivo = Path.Combine(rutaCarpeta, nombreUnico);

            // Guardar archivo en servidor
            using (var stream = new FileStream(rutaCompletaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Construir la URL completa
            var request = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}";
            string urlAbsoluta = $"{baseUrl}/{rutaRelativaCarpeta}/{nombreUnico}";

            return ApiResponse<string>.SuccessResult(urlAbsoluta, "Archivo subido exitosamente.");
        }
    }
}
