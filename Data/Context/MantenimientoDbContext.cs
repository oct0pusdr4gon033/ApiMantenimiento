using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Models.Entitys.MSecurity;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using ApiMantenimiento.Data.Configurations.MFlota;

namespace ApiMantenimiento.Data.Context
{
    public class MantenimientoDbContext : DbContext 
    {
        public MantenimientoDbContext(DbContextOptions<MantenimientoDbContext> options) : base(options) 
        {
        }

        // 1. AQUI VAN TUS DBSETS (Tus "puertas" a las tablas)
        public DbSet<AreaOperacion> AreaOperaciones { get; set; }
        public DbSet<MarcaEquipo> MarcaEquipos { get; set; }
        public DbSet<TipoEquipo> TipoEquipos { get; set; }
        public DbSet<ModeloEquipo> ModeloEquipos { get; set; }
        public DbSet<Flota> Flotas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ExpedienteDocumento> ExpedienteDocumentos { get; set; }
        public DbSet<HistorialHorometros> HistorialHorometros { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<ExpedienteEmpleado> ExpedienteEmpleados { get; set; }
        public DbSet<TipoDocumentoEmpleado> TipoDocumentoEmpleados { get; set; }
        public DbSet<ExpedienteDocumentoEmpleado> ExpedienteDocumentoEmpleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
        public DbSet<Estrategia> Estrategias { get; set; }
        public DbSet<EstrategiaDetalle> EstrategiaDetalles { get; set; }
        public DbSet<SistemaEquipo> SistemasEquipos { get; set; }
        public DbSet<SubSistemaEquipo> SubSistemasEquipos { get; set; }
        public DbSet<ActividadSistema> ActividadesSistemas { get; set; }

        // MAlmacen
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.UnidadMedida> UnidadesMedida { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.CategoriaMaterial> CategoriasMaterial { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.Material> Materiales { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.HistorialPrecio> HistorialPrecios { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.Vale> Vales { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.ValeMaterial> ValesMateriales { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MAlmacen.MovimientoInventario> MovimientosInventario { get; set; }

        // MMantenimiento - Plan
        public DbSet<PlanMantenimiento> PlanesMantenimiento { get; set; }
        public DbSet<PlanMantenimientoActividad> PlanesMantenimientoActividades { get; set; }
        public DbSet<PlanMantenimientoPersonal> PlanesMantenimientoPersonales { get; set; }
        public DbSet<PlanActividadMaterial> PlanesActividadMaterial { get; set; }

        // MMantenimiento - Órdenes de Trabajo
        public DbSet<OrdenTrabajo> OrdenesTrabajoMant { get; set; }
        public DbSet<OTPlanDetalle> OTPlanesDetalle { get; set; }
        public DbSet<OTActividad> OTActividades { get; set; }
        public DbSet<OTMaterial> OTMateriales { get; set; }
        public DbSet<OTPersonal> OTPersonal { get; set; }
        public DbSet<PMUltimaIntervencion> PMUltimasIntervenciones { get; set; }
        public DbSet<CalendarioMantenimiento> CalendariosMantenimiento { get; set; }
        public DbSet<ConfiguracionFlota> ConfiguracionesFlota { get; set; }

        // MCompras (Módulo de Compras)
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.Proveedor> Proveedores { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.ProveedorContacto> ProveedorContactos { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.CategoriaProveedor> CategoriaProveedores { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.ProveedorCategoria> ProveedorCategorias { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.SolicitudPedido> SolicitudesPedidos { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.SolicitudPedidoDetalle> SolicitudPedidoDetalles { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.Cotizacion> Cotizaciones { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.CotizacionDetalle> CotizacionDetalles { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.OrdenCompra> OrdenesCompras { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.OrdenCompraDetalle> OrdenCompraDetalles { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.NotaIngreso> NotasIngresos { get; set; }
        public DbSet<ApiMantenimiento.Models.Entitys.MCompras.NotaIngresoDetalle> NotaIngresoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MantenimientoDbContext).Assembly);  
        }
    }
}
