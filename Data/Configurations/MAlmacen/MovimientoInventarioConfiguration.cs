using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class MovimientoInventarioConfiguration : IEntityTypeConfiguration<MovimientoInventario>
    {
        public void Configure(EntityTypeBuilder<MovimientoInventario> builder)
        {
            builder.ToTable("Alm_MovimientoInventario");
            builder.HasKey(e => e.id_movimiento);
            builder.Property(e => e.id_movimiento).ValueGeneratedOnAdd();

            builder.Property(e => e.tipo_movimiento).IsRequired().HasMaxLength(10); // ENTRADA | SALIDA
            builder.Property(e => e.cantidad).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.saldo_stock).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.origen_tipo).IsRequired().HasMaxLength(20); // INICIAL | ENTRADA_MANUAL | NOTA_SALIDA | AJUSTE
            builder.Property(e => e.origen_referencia).IsRequired().HasMaxLength(100);
            builder.Property(e => e.responsable).IsRequired().HasMaxLength(150);
            builder.Property(e => e.observaciones).HasMaxLength(500);

            // Relación con Material (Muchos a Uno)
            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
