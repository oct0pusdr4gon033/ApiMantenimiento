using ApiMantenimiento.Models.Entitys.MEmpleados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MEmpleados
{
    public class ExpedienteEmpleadoConfiguration : IEntityTypeConfiguration<ExpedienteEmpleado>
    {
        public void Configure(EntityTypeBuilder<ExpedienteEmpleado> builder)
        {
            builder.ToTable("ExpedienteEmpleado");

            // Llave primaria
            builder.HasKey(x => x.codigo_exp_emp);

            builder.Property(x => x.codigo_exp_emp)
                   .HasColumnType("varchar(20)");

            // Relación 1 a 1 con Empleado
            builder.HasIndex(x => x.dni_empleado).IsUnique();

            builder.HasOne(x => x.Empleado)
                   .WithOne(e => e.ExpedienteEmpleado)
                   .HasForeignKey<ExpedienteEmpleado>(x => x.dni_empleado)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación 1 a Muchos con ExpedienteDocumentoEmpleado
            builder.HasMany(x => x.DetallesDocumento)
                   .WithOne(ed => ed.ExpedienteEmpleado)
                   .HasForeignKey(ed => ed.codigo_exp_emp)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
