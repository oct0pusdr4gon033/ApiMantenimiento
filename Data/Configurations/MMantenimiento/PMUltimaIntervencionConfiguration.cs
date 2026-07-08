using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class PMUltimaIntervencionConfiguration : IEntityTypeConfiguration<PMUltimaIntervencion>
    {
        public void Configure(EntityTypeBuilder<PMUltimaIntervencion> builder)
        {
            builder.ToTable("Man_PMUltimaIntervencion");
            builder.HasKey(e => new { e.id_equipo, e.id_detalle_estrg });

            builder.Property(e => e.horometro_corte).HasColumnType("decimal(18,2)").HasDefaultValue(0m);

            // FK → Equipo (dirección: PMUltimaIntervencion → Equipo, no circular)
            builder.HasOne(e => e.Equipo)
                .WithMany()
                .HasForeignKey(e => e.id_equipo)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → EstrategiaDetalle
            builder.HasOne(e => e.EstrategiaDetalle)
                .WithMany()
                .HasForeignKey(e => e.id_detalle_estrg)
                .OnDelete(DeleteBehavior.Restrict);

            // FK → OrdenTrabajo (nullable, unidireccional: PMUlt → OT, OT no referencia PMUlt)
            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany()
                .HasForeignKey(e => e.id_ot)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
