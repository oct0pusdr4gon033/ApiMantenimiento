using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class PlanMantenimientoActividadConfiguration : IEntityTypeConfiguration<PlanMantenimientoActividad>
    {
        public void Configure(EntityTypeBuilder<PlanMantenimientoActividad> builder)
        {
            builder.ToTable("Man_PlanMantenimientoActividad");

            // PK compuesta natural — sin surrogate
            builder.HasKey(e => new { e.id_plan_mant, e.id_actividad, e.id_detalle_estrg });

            // FK → Man_PlanMantenimiento
            builder.HasOne(e => e.PlanMantenimiento)
                .WithMany(p => p.PlanMantenimientoActividades)
                .HasForeignKey(e => e.id_plan_mant)
                .OnDelete(DeleteBehavior.Cascade);

            // FK → ActividadSistema
            builder.HasOne(e => e.ActividadSistema)
                .WithMany()
                .HasForeignKey(e => e.id_actividad)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → EstrategiaDetalle
            builder.HasOne(e => e.EstrategiaDetalle)
                .WithMany()
                .HasForeignKey(e => e.id_detalle_estrg)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
