using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class ConfiguracionFlotaConfiguration : IEntityTypeConfiguration<ConfiguracionFlota>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionFlota> builder)
        {
            builder.ToTable("Man_ConfiguracionFlota");
            builder.HasKey(e => e.id_configuracion);

            builder.Property(e => e.horas_diarias_estimadas)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(12m)
                .IsRequired();

            builder.Property(e => e.actualizado_por).HasMaxLength(50);

            // FK → Flota (nullable — null = configuración global)
            builder.HasOne(e => e.Flota)
                .WithMany()
                .HasForeignKey(e => e.id_flota)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
