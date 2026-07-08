using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Alm_Material");
            builder.HasKey(e => e.id_material);
            builder.Property(e => e.id_material).ValueGeneratedOnAdd();
            builder.Property(e => e.cod_materia).IsRequired().HasMaxLength(50);
            builder.Property(e => e.descripcion).IsRequired().HasMaxLength(500);
            builder.Property(e => e.estado).IsRequired().HasMaxLength(20);
            
            // Decimal scale/precision for stock
            builder.Property(e => e.stock).HasColumnType("decimal(18,2)");
            builder.Property(e => e.stock_minimo).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            builder.Property(e => e.precio_actual).HasColumnType("decimal(18,2)").HasDefaultValue(0);

            // Unique constraint on cod_materia as user requested validation
            builder.HasIndex(e => e.cod_materia).IsUnique();

            // Foreign keys
            builder.HasOne(e => e.UnidadMedida)
                .WithMany()
                .HasForeignKey(e => e.id_unidad)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CategoriaMaterial)
                .WithMany()
                .HasForeignKey(e => e.id_categoria)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
