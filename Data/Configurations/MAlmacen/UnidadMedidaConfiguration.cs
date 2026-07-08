using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class UnidadMedidaConfiguration : IEntityTypeConfiguration<UnidadMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadMedida> builder)
        {
            builder.ToTable("Alm_UnidadMedida");
            builder.HasKey(e => e.id_unidad);
            builder.Property(e => e.id_unidad).ValueGeneratedOnAdd();
            builder.Property(e => e.nombre_unidad).IsRequired().HasMaxLength(100);
            builder.Property(e => e.abreviatura).IsRequired().HasMaxLength(10);
        }
    }
}
