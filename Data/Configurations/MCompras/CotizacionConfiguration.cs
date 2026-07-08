using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class CotizacionConfiguration : IEntityTypeConfiguration<Cotizacion>
    {
        public void Configure(EntityTypeBuilder<Cotizacion> builder)
        {
            builder.ToTable("Com_Cotizacion");
            builder.HasKey(e => e.id_cotizacion);
            builder.Property(e => e.id_cotizacion).ValueGeneratedOnAdd();
            builder.Property(e => e.nro_cotizacion).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.ruc_proveedor).HasColumnType("varchar(20)").IsRequired();
            builder.Property(e => e.fecha_cotizacion).IsRequired();
            builder.Property(e => e.estado).HasColumnType("varchar(20)").HasDefaultValue("PENDIENTE").IsRequired();
            builder.Property(e => e.total).HasColumnType("decimal(18,2)").HasDefaultValue(0).IsRequired();

            // Indexes
            builder.HasIndex(e => e.nro_cotizacion).IsUnique();

            // Relations
            builder.HasOne(e => e.SolicitudPedido)
                .WithMany()
                .HasForeignKey(e => e.id_solicitud_pedido)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Proveedor)
                .WithMany()
                .HasForeignKey(e => e.ruc_proveedor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
