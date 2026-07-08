using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class PlanActividadMaterialConfiguration : IEntityTypeConfiguration<PlanActividadMaterial>
    {
        public void Configure(EntityTypeBuilder<PlanActividadMaterial> builder)
        {
            builder.ToTable("Man_PlanActividadMaterial");

            // PK compuesta: actividad dentro del plan + material
            builder.HasKey(e => new { e.id_plan_mant, e.id_actividad, e.id_detalle_estrg, e.id_material });

            builder.Property(e => e.cantidad)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // FK compuesta → Man_PlanMantenimientoActividad
            builder.HasOne(e => e.PlanMantenimientoActividad)
                .WithMany(a => a.Materiales)
                .HasForeignKey(e => new { e.id_plan_mant, e.id_actividad, e.id_detalle_estrg })
                .OnDelete(DeleteBehavior.Cascade);

            // FK → Material
            builder.HasOne(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.id_material)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
