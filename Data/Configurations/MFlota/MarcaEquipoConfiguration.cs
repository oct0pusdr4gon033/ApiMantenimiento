using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class MarcaEquipoConfiguration : IEntityTypeConfiguration <MarcaEquipo>
    {
        public void Configure(EntityTypeBuilder<MarcaEquipo> builder)
        {
            builder.ToTable("MarcaEquipo");

            // Llave Primaria (PK)
            builder.HasKey(x => x.id_marca);
            //nombre_marca
            builder.Property(x => x.nombre_marca).HasColumnName("nombre_marca").HasMaxLength(10);
        }

    }
}
