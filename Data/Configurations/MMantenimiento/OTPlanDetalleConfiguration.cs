using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class OTPlanDetalleConfiguration : IEntityTypeConfiguration<OTPlanDetalle>
    {
        public void Configure(EntityTypeBuilder<OTPlanDetalle> builder)
        {
            builder.ToTable("Man_OTPlanDetalle");
            builder.HasKey(e => new { e.id_ot, e.id_detalle_estrg });

            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany(o => o.PlanesDetalle)
                .HasForeignKey(e => e.id_ot)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.EstrategiaDetalle)
                .WithMany()
                .HasForeignKey(e => e.id_detalle_estrg)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
