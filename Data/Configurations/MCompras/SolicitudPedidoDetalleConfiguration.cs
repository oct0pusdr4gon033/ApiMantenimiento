using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class SolicitudPedidoDetalleConfiguration : IEntityTypeConfiguration<SolicitudPedidoDetalle>
    {
        public void Configure(EntityTypeBuilder<SolicitudPedidoDetalle> builder)
        {
            builder.ToTable("Com_SolicitudPedidoDetalle");
            builder.HasKey(e => e.id_detalle);
            builder.Property(e => e.id_detalle).ValueGeneratedOnAdd();
            builder.Property(e => e.cod_materia).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.nombre).HasColumnType("varchar(250)").IsRequired();
            builder.Property(e => e.stock_minimo).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            builder.Property(e => e.cantidad_pedida).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.ruc_proveedor).HasColumnType("varchar(20)");
            builder.Property(e => e.es_nuevo_producto).IsRequired();
            builder.Property(e => e.especificaciones).HasColumnType("varchar(1000)");

            // Relations
            builder.HasOne(e => e.SolicitudPedido)
                .WithMany(s => s.Detalles)
                .HasForeignKey(e => e.id_solicitud_pedido)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CategoriaMaterial)
                .WithMany()
                .HasForeignKey(e => e.id_categoria)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UnidadMedida)
                .WithMany()
                .HasForeignKey(e => e.id_unidad)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Proveedor)
                .WithMany()
                .HasForeignKey(e => e.ruc_proveedor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
