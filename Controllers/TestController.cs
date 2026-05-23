using Microsoft.AspNetCore.Mvc;
using ApiMantenimiento.Data.Context;
namespace ApiMantenimiento.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly MantenimientoDbContext _context; 
        
        public TestController(MantenimientoDbContext context)
        {
            _context = context;
        }
        [HttpGet("conexion")]
        public IActionResult Conexion()
        {
            bool conectado = _context.Database.CanConnect();
            if (conectado)
            {
                return Ok("Conexion Exitosa");
            }
            return BadRequest("Error de Conexion");
        }
        
        
    }
}
