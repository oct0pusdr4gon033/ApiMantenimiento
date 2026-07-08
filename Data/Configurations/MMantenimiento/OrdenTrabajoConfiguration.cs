using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class OrdenTrabajoConfiguration : IEntityTypeConfiguration<OrdenTrabajo>
    {
        public void Configure(EntityTypeBuilder<OrdenTrabajo> builder)
        {
            builder.ToTable("Man_OrdenTrabajo");
            builder.HasKey(e => e.id_ot);

            builder.Property(e => e.cod_ot).IsRequired().HasMaxLength(20);
            builder.HasIndex(e => e.cod_ot).IsUnique();

            builder.Property(e => e.tipo_ot).IsRequired().HasMaxLength(12);
            builder.Property(e => e.forma_generacion).IsRequired().HasMaxLength(6);
            builder.Property(e => e.estado).IsRequired().HasMaxLength(15).HasDefaultValue("PENDIENTE");
            builder.Property(e => e.horometro_al_momento).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.horometro_corte).HasColumnType("decimal(18,2)");
            builder.Property(e => e.fecha_creacion).IsRequired();
            builder.Property(e => e.observaciones).HasMaxLength(500);
            builder.Property(e => e.creado_por).HasMaxLength(50);

            // Campos de Correctivas
            builder.Property(e => e.hora_intervencion);
            builder.Property(e => e.descripcion_falla).HasMaxLength(500);
            builder.Property(e => e.horometro_falla).HasColumnType("decimal(18,2)");

            // FK → Equipo (datos maestros, sin ciclo)
            builder.HasOne(e => e.Equipo)
                .WithMany()
                .HasForeignKey(e => e.id_equipo)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → PlanMantenimiento (datos maestros, sin ciclo)
            builder.HasOne(e => e.PlanMantenimiento)
                .WithMany()
                .HasForeignKey(e => e.id_plan_mant)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → Sistema (opcional, para correctiva)
            builder.HasOne(e => e.Sistema)
                .WithMany()
                .HasForeignKey(e => e.id_sistema)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → SubSistema (opcional, para correctiva)
            builder.HasOne(e => e.SubSistema)
                .WithMany()
                .HasForeignKey(e => e.id_subsistema)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
