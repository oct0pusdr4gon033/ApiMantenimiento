using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class CalendarioMantenimientoConfiguration : IEntityTypeConfiguration<CalendarioMantenimiento>
    {
        public void Configure(EntityTypeBuilder<CalendarioMantenimiento> builder)
        {
            builder.ToTable("Man_CalendarioMantenimiento");
            builder.HasKey(e => e.id_calendario);

            builder.Property(e => e.horometro_base).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.horas_diarias_usadas).HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(e => e.ejecutado).HasDefaultValue(false);

            // FK → Equipo
            builder.HasOne(e => e.Equipo)
                .WithMany()
                .HasForeignKey(e => e.id_equipo)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → EstrategiaDetalle
            builder.HasOne(e => e.EstrategiaDetalle)
                .WithMany()
                .HasForeignKey(e => e.id_detalle_estrg)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → OrdenTrabajo (nullable, unidireccional: Calendario → OT)
            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany()
                .HasForeignKey(e => e.id_ot)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Un solo registro activo (no ejecutado) por equipo+PM
            builder.HasIndex(e => new { e.id_equipo, e.id_detalle_estrg, e.ejecutado });
        }
    }
}
