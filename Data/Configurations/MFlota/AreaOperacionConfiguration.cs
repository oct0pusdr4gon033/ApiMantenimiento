using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class AreaOperacionConfiguration : IEntityTypeConfiguration<AreaOperacion>
    {
        public void Configure(EntityTypeBuilder<AreaOperacion> builder)
        {
            builder.ToTable("AreaOperacion");

            builder.HasKey(x => x.cod_area_ope);

            builder.Property(x => x.cod_area_ope).
                HasColumnName("cod_area_ope").HasColumnType("varchar(10)");

            builder.Property(x => x.cod_area_ope).
                HasMaxLength(10);

            builder.Property(x => x.nomb_area).
                HasColumnName("nomb_area").HasMaxLength(20);
        }
    }
}
