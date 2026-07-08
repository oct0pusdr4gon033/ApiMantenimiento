using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class ProveedorContactoConfiguration : IEntityTypeConfiguration<ProveedorContacto>
    {
        public void Configure(EntityTypeBuilder<ProveedorContacto> builder)
        {
            builder.ToTable("Com_ProveedorContacto");
            builder.HasKey(e => e.id_contacto);
            builder.Property(e => e.id_contacto).ValueGeneratedOnAdd();
            builder.Property(e => e.ruc_proveedor).HasColumnType("varchar(20)").IsRequired();
            builder.Property(e => e.nombre).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.apellido1).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.apellido2).HasColumnType("varchar(100)");
            builder.Property(e => e.correo).HasColumnType("varchar(150)");
            builder.Property(e => e.telefono).HasColumnType("varchar(50)");
            builder.Property(e => e.estado).HasColumnType("varchar(20)").HasDefaultValue("ACTIVO");

            builder.HasOne(e => e.Proveedor)
                .WithMany(p => p.Contactos)
                .HasForeignKey(e => e.ruc_proveedor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
