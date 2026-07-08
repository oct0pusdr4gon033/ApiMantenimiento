using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class SolicitudPedidoConfiguration : IEntityTypeConfiguration<SolicitudPedido>
    {
        public void Configure(EntityTypeBuilder<SolicitudPedido> builder)
        {
            builder.ToTable("Com_SolicitudPedido");
            builder.HasKey(e => e.id_solicitud_pedido);
            builder.Property(e => e.id_solicitud_pedido).ValueGeneratedOnAdd();
            builder.Property(e => e.cod_solicitud).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.dni_empleado).HasColumnType("char(8)").IsRequired();
            builder.Property(e => e.fecha_creacion).IsRequired();
            builder.Property(e => e.estado).HasColumnType("varchar(20)").HasDefaultValue("PENDIENTE").IsRequired();

            // Unique index on cod_solicitud
            builder.HasIndex(e => e.cod_solicitud).IsUnique();

            // Relation with Empleado (primary key is dni_empleado in Empleado table)
            builder.HasOne(e => e.Empleado)
                .WithMany()
                .HasForeignKey(e => e.dni_empleado)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
