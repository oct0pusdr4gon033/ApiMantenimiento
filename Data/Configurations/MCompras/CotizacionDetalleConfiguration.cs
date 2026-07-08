using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class CotizacionDetalleConfiguration : IEntityTypeConfiguration<CotizacionDetalle>
    {
        public void Configure(EntityTypeBuilder<CotizacionDetalle> builder)
        {
            builder.ToTable("Com_CotizacionDetalle");
            builder.HasKey(e => e.id_cotizacion_detalle);
            builder.Property(e => e.id_cotizacion_detalle).ValueGeneratedOnAdd();
            builder.Property(e => e.cantidad).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.precio_unitario).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.subtotal).HasColumnType("decimal(18,2)").IsRequired();

            // Relations
            builder.HasOne(e => e.Cotizacion)
                .WithMany(c => c.Detalles)
                .HasForeignKey(e => e.id_cotizacion)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
