using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class NotaIngresoConfiguration : IEntityTypeConfiguration<NotaIngreso>
    {
        public void Configure(EntityTypeBuilder<NotaIngreso> builder)
        {
            builder.ToTable("Com_NotaIngreso");
            builder.HasKey(e => e.id_nota_ingreso);
            builder.Property(e => e.id_nota_ingreso).ValueGeneratedOnAdd();
            builder.Property(e => e.nro_nota).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.fecha_ingreso).IsRequired();
            builder.Property(e => e.estado).HasColumnType("varchar(20)").HasDefaultValue("PROCESADO").IsRequired();
            builder.Property(e => e.observaciones).HasColumnType("varchar(500)");

            // Indexes
            builder.HasIndex(e => e.nro_nota).IsUnique();

            // Relations
            builder.HasOne(e => e.OrdenCompra)
                .WithMany()
                .HasForeignKey(e => e.id_orden_compra)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
