using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class PlanMantenimientoPersonalConfiguration : IEntityTypeConfiguration<PlanMantenimientoPersonal>
    {
        public void Configure(EntityTypeBuilder<PlanMantenimientoPersonal> builder)
        {
            builder.ToTable("Man_PlanMantenimientoPersonal");
            builder.HasKey(e => e.id_plan_personal);
            builder.Property(e => e.id_plan_personal).ValueGeneratedOnAdd();
            builder.Property(e => e.cantidad).IsRequired();

            // Foreign Keys
            builder.HasOne(e => e.PlanMantenimiento)
                .WithMany(p => p.PlanMantenimientoPersonales)
                .HasForeignKey(e => e.id_plan_mant)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Rol)
                .WithMany()
                .HasForeignKey(e => e.id_rol)
                .OnDelete(DeleteBehavior.Restrict);

            // Validation 3NF: Evitar roles duplicados en un mismo plan
            builder.HasIndex(e => new { e.id_plan_mant, e.id_rol }).IsUnique();
        }
    }
}
