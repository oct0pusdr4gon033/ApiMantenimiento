using ApiMantenimiento.Models.Requests.MSecurity;

namespace ApiMantenimiento.Services.Interfaces.MSecurity
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
