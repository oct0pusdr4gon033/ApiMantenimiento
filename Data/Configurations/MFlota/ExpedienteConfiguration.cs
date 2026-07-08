using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class ExpedienteConfiguration : IEntityTypeConfiguration<Expediente>
    {
        public void Configure(EntityTypeBuilder<Expediente> builder)
        {
            builder.ToTable("Expediente");

            // Llave primaria
            builder.HasKey(x => x.codigo_exp);

            builder.Property(x => x.codigo_exp)
                   .HasColumnType("varchar(20)");

            // Relación 1 a 1 con Equipo ya está configurada en EquipoConfiguration
            // pero podemos asegurar que id_equipo es único si es 1 a 1
            builder.HasIndex(x => x.id_equipo).IsUnique();

            // Relación 1 a Muchos con ExpedienteDocumento
            builder.HasMany(x => x.DetallesDocumento)
                   .WithOne(ed => ed.Expediente)
                   .HasForeignKey(ed => ed.codigo_exp)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
