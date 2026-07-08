using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class OTPersonalConfiguration : IEntityTypeConfiguration<OTPersonal>
    {
        public void Configure(EntityTypeBuilder<OTPersonal> builder)
        {
            builder.ToTable("Man_OTPersonal");
            builder.HasKey(e => e.id_ot_personal);

            builder.Property(e => e.dni_empleado).IsRequired().HasMaxLength(15);

            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany(o => o.Personal)
                .HasForeignKey(e => e.id_ot)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Empleado)
                .WithMany()
                .HasForeignKey(e => e.dni_empleado)
                .HasPrincipalKey(emp => emp.dni_empleado)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
