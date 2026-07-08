using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class PlanMantenimientoConfiguration : IEntityTypeConfiguration<PlanMantenimiento>
    {
        public void Configure(EntityTypeBuilder<PlanMantenimiento> builder)
        {
            builder.ToTable("Man_PlanMantenimiento");
            builder.HasKey(e => e.id_plan_mant);
            builder.Property(e => e.id_plan_mant).ValueGeneratedOnAdd();
            builder.Property(e => e.fecha_creacion).IsRequired();
            builder.Property(e => e.estado).IsRequired();

            // Foreign Key
            builder.HasOne(e => e.Estrategia)
                .WithMany()
                .HasForeignKey(e => e.id_estrategia)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
