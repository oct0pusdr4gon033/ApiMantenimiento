using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class OrdenCompraConfiguration : IEntityTypeConfiguration<OrdenCompra>
    {
        public void Configure(EntityTypeBuilder<OrdenCompra> builder)
        {
            builder.ToTable("Com_OrdenCompra");
            builder.HasKey(e => e.id_orden_compra);
            builder.Property(e => e.id_orden_compra).ValueGeneratedOnAdd();
            builder.Property(e => e.nro_orden).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.ruc_proveedor).HasColumnType("varchar(20)").IsRequired();
            builder.Property(e => e.fecha_orden).IsRequired();
            builder.Property(e => e.estado).HasColumnType("varchar(20)").HasDefaultValue("PENDIENTE").IsRequired();
            builder.Property(e => e.total).HasColumnType("decimal(18,2)").HasDefaultValue(0).IsRequired();

            // Indexes
            builder.HasIndex(e => e.nro_orden).IsUnique();

            // Relations
            builder.HasOne(e => e.Cotizacion)
                .WithMany()
                .HasForeignKey(e => e.id_cotizacion)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Proveedor)
                .WithMany()
                .HasForeignKey(e => e.ruc_proveedor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
