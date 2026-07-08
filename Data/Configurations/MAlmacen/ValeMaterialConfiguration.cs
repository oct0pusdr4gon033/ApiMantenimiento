using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class ValeMaterialConfiguration : IEntityTypeConfiguration<ValeMaterial>
    {
        public void Configure(EntityTypeBuilder<ValeMaterial> builder)
        {
            builder.ToTable("Alm_ValeMaterial");
            builder.HasKey(e => e.id_vale_material);
            builder.Property(e => e.id_vale_material).ValueGeneratedOnAdd();

            builder.Property(e => e.cantidad_solicitada).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.cantidad_despachada).HasColumnType("decimal(18,2)");

            // Relación con Vale (Muchos a Uno)
            builder.HasOne(e => e.Vale)
                .WithMany(v => v.Materiales)
                .HasForeignKey(e => e.id_vale)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con Material (Muchos a Uno)
            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
