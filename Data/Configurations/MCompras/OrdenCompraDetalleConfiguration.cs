using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class OrdenCompraDetalleConfiguration : IEntityTypeConfiguration<OrdenCompraDetalle>
    {
        public void Configure(EntityTypeBuilder<OrdenCompraDetalle> builder)
        {
            builder.ToTable("Com_OrdenCompraDetalle");
            builder.HasKey(e => e.id_orden_detalle);
            builder.Property(e => e.id_orden_detalle).ValueGeneratedOnAdd();
            builder.Property(e => e.cantidad).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.precio_unitario).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.subtotal).HasColumnType("decimal(18,2)").IsRequired();

            // Relations
            builder.HasOne(e => e.OrdenCompra)
                .WithMany(o => o.Detalles)
                .HasForeignKey(e => e.id_orden_compra)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
