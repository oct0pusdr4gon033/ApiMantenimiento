using ApiMantenimiento.Models.Entitys.MEmpleados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MEmpleados
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol");

            // Llave primaria
            builder.HasKey(x => x.id_rol);

            // Mapeo
            builder.Property(x => x.nombre_rol).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.prefijo).HasColumnType("varchar(10)").IsRequired();
        }
    }
}
