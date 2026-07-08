using ApiMantenimiento.Models.Entitys.MSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MSecurity
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.dni_empleado);

            builder.Property(x => x.dni_empleado)
                   .HasColumnType("char(8)")
                   .IsRequired();

            builder.Property(x => x.email)
                   .HasColumnType("varchar(100)")
                   .IsRequired();

            builder.Property(x => x.password)
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            // Relación 1 a 0..1 con Empleado
            builder.HasOne(x => x.Empleado)
                   .WithOne()
                   .HasForeignKey<Usuario>(x => x.dni_empleado)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
