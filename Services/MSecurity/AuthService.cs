using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Models.Entitys.MSecurity;
using ApiMantenimiento.Models.Requests.MSecurity;
using ApiMantenimiento.Services.Interfaces.MSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiMantenimiento.Services.MSecurity
{
    public class AuthService : IAuthService
    {
        private readonly MantenimientoDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(MantenimientoDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .ThenInclude(e => e.Rol)
                .FirstOrDefaultAsync(u => u.email == request.Email);

            if (usuario == null)
            {
                throw new Exception("Credenciales incorrectas");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.password))
            {
                throw new Exception("Credenciales incorrectas");
            }

            if (!usuario.Empleado.estado)
            {
                throw new Exception("El empleado está inactivo");
            }

            return GenerateJwtToken(usuario);
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Rol)
                .FirstOrDefaultAsync(e => e.dni_empleado == request.Dni);

            if (empleado == null)
            {
                throw new Exception("El empleado no existe");
            }

            if (!empleado.estado)
            {
                throw new Exception("El empleado no está activo");
            }

            if (empleado.Rol == null || !empleado.Rol.nombre_rol.Equals("Jefe", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("El empleado debe tener el rol de Jefe para registrarse");
            }

            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.dni_empleado == request.Dni || u.email == request.Email);
            if (existingUser != null)
            {
                throw new Exception("El usuario ya existe (DNI o Email duplicado)");
            }

            var newUser = new Usuario
            {
                dni_empleado = request.Dni,
                email = request.Email,
                password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.dni_empleado),
                new Claim(ClaimTypes.Email, usuario.email),
                new Claim("Nombre", usuario.Empleado.nombre),
                new Claim("Apellido1", usuario.Empleado.apellido1),
                new Claim(ClaimTypes.Role, usuario.Empleado.Rol?.nombre_rol ?? "")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
