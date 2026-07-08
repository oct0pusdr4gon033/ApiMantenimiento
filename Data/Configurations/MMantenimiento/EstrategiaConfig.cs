using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiMantenimiento.Models.Entitys.MMantenimiento;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class EstrategiaConfig : IEntityTypeConfiguration<Estrategia>
    {
        public void Configure(EntityTypeBuilder<Estrategia> builder)
        {
            builder.ToTable("Estrategia");

            builder.HasKey(e => e.id_estrategia);

            builder.Property(e => e.id_estrategia)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.cod_estrategia)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.titulo_estrategia)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.estado)
                   .HasMaxLength(50);

            // Relaciones opcionales: una estrategia puede pertenecer a una flota o a un equipo
            builder.HasOne(e => e.Flota)
                   .WithMany()
                   .HasForeignKey(e => e.id_flota)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Equipo)
                   .WithMany()
                   .HasForeignKey(e => e.id_equipo)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
