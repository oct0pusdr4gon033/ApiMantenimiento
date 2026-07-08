using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class ActividadSistemaConfiguration : IEntityTypeConfiguration<ActividadSistema>
    {
        public void Configure(EntityTypeBuilder<ActividadSistema> builder)
        {
            builder.ToTable("actividades_sistema");

            builder.HasKey(e => e.id_actividad);

            builder.Property(e => e.id_actividad)
                   .HasColumnName("id_actividad")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.cod_act)
                   .HasColumnName("cod_act")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.id_sistema)
                   .HasColumnName("id_sistema")
                   .IsRequired();

            builder.Property(e => e.nombre_actividad)
                   .HasColumnName("nombre_actividad")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.descripcion)
                   .HasColumnName("descripcion")
                   .HasColumnType("text");

            builder.Property(e => e.duracion)
                   .HasColumnName("duracion");

            builder.Property(e => e.medida_duracion)
                   .HasColumnName("medida_duracion")
                   .HasMaxLength(5)
                   .IsRequired();

            builder.Property(e => e.estado)
                   .HasColumnName("estado");

            // Relationships
            builder.HasOne(a => a.SistemaEquipo)
                   .WithMany(s => s.Actividades)
                   .HasForeignKey(a => a.id_sistema)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
