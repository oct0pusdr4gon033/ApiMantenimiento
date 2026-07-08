using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class SubSistemaEquipoConfiguration : IEntityTypeConfiguration<SubSistemaEquipo>
    {
        public void Configure(EntityTypeBuilder<SubSistemaEquipo> builder)
        {
            builder.ToTable("subsistema_equipo");

            builder.HasKey(e => e.id_subsistema);

            builder.Property(e => e.id_subsistema)
                   .HasColumnName("id_subsistema")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.cod_subsist)
                   .HasColumnName("cod_subsist")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.nombre_subsist)
                   .HasColumnName("nombre_subsist")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.id_sistema)
                   .HasColumnName("id_sistema")
                   .IsRequired();

            builder.HasOne(e => e.Sistema)
                   .WithMany(s => s.SubSistemas)
                   .HasForeignKey(e => e.id_sistema)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
