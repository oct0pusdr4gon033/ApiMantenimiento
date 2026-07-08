using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class NotaIngresoDetalleConfiguration : IEntityTypeConfiguration<NotaIngresoDetalle>
    {
        public void Configure(EntityTypeBuilder<NotaIngresoDetalle> builder)
        {
            builder.ToTable("Com_NotaIngresoDetalle");
            builder.HasKey(e => e.id_nota_detalle);
            builder.Property(e => e.id_nota_detalle).ValueGeneratedOnAdd();
            builder.Property(e => e.cantidad).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.precio_unitario).HasColumnType("decimal(18,2)").IsRequired();

            // Relations
            builder.HasOne(e => e.NotaIngreso)
                .WithMany(n => n.Detalles)
                .HasForeignKey(e => e.id_nota_ingreso)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
