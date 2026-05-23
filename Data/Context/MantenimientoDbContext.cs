using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using ApiMantenimiento.Data.Configurations.MFlota;
namespace ApiMantenimiento.Data.Context
{
    public class MantenimientoDbContext : DbContext 
    {
        public MantenimientoDbContext(DbContextOptions
            <MantenimientoDbContext> options) : base(options) {

        }
        // 1. AQUI VAN TUS DBSETS (Tus "puertas" a las tablas)
        public DbSet<AreaOperacion> AreaOperaciones { get; set; }
        public DbSet<MarcaEquipo> MarcaEquipos { get; set; }
        public DbSet<TipoEquipo> TipoEquipos { get; set; }
        public DbSet<ModeloEquipo> ModeloEquipos { get; set; }
        public DbSet<Flota> Flotas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MantenimientoDbContext).Assembly);  
        }
  
    
    }
}
