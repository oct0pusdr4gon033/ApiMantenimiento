using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class HistorialPrecioConfiguration : IEntityTypeConfiguration<HistorialPrecio>
    {
        public void Configure(EntityTypeBuilder<HistorialPrecio> builder)
        {
            builder.ToTable("Alm_HistorialPrecio");
            builder.HasKey(e => e.id_historial);
            builder.Property(e => e.id_historial).ValueGeneratedOnAdd();
            builder.Property(e => e.precio).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.fecha_registro).IsRequired();

            builder.HasOne(e => e.Material)
                .WithMany(m => m.HistorialPrecios)
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
