using ApiMantenimiento.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace ApiMantenimiento.Services.Interfaces.MFlota
{
    public interface IStorageService
    {
        Task<ApiResponse<string>> SubirArchivoAsync(IFormFile archivo, string version, string modulo);
    }
}
