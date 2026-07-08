using ApiMantenimiento.Models.Entitys.MEmpleados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MEmpleados
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado");

            // Llave primaria: usaremos dni_empleado como base, aunque ahora tiene codigo_empleado
            builder.HasKey(x => x.dni_empleado);

            // Propiedades
            builder.Property(x => x.dni_empleado).HasColumnType("char(8)").IsRequired();
            builder.Property(x => x.codigo_empleado).HasColumnType("varchar(20)");
            builder.Property(x => x.nombre).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.apellido1).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.apellido2).HasColumnType("varchar(100)");
            builder.Property(x => x.telf).HasColumnType("varchar(20)");
            builder.Property(x => x.email).HasColumnType("varchar(100)");
            builder.Property(x => x.estado).HasDefaultValue(true);

            // Relación con Rol
            builder.HasOne(x => x.Rol)
                   .WithMany()
                   .HasForeignKey(x => x.id_rol)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
