using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class CategoriaMaterialConfiguration : IEntityTypeConfiguration<CategoriaMaterial>
    {
        public void Configure(EntityTypeBuilder<CategoriaMaterial> builder)
        {
            builder.ToTable("Alm_CategoriaMaterial");
            builder.HasKey(e => e.id_categoria);
            builder.Property(e => e.id_categoria).ValueGeneratedOnAdd();
            builder.Property(e => e.cod_cat).IsRequired().HasMaxLength(20);
            builder.Property(e => e.nombre_categoria).IsRequired().HasMaxLength(150);

            builder.HasIndex(e => e.cod_cat).IsUnique(); // Validate uniqueness
        }
    }
}
