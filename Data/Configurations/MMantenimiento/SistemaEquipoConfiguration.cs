using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class SistemaEquipoConfiguration : IEntityTypeConfiguration<SistemaEquipo>
    {
        public void Configure(EntityTypeBuilder<SistemaEquipo> builder)
        {
            builder.ToTable("sistema_equipo");

            builder.HasKey(e => e.id_sistema);

            builder.Property(e => e.id_sistema)
                   .HasColumnName("id_sistema")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.cod_sist)
                   .HasColumnName("cod_sist")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.nombre_sist)
                   .HasColumnName("nombre_sist")
                   .HasMaxLength(200)
                   .IsRequired();
        }
    }
}
