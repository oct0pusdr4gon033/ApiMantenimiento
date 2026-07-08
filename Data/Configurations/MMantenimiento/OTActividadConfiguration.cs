using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class OTActividadConfiguration : IEntityTypeConfiguration<OTActividad>
    {
        public void Configure(EntityTypeBuilder<OTActividad> builder)
        {
            builder.ToTable("Man_OTActividad");
            builder.HasKey(e => e.id_ot_actividad);

            builder.Property(e => e.nombre_actividad).IsRequired().HasMaxLength(200);
            builder.Property(e => e.cod_sistema).IsRequired(false).HasMaxLength(20);
            builder.Property(e => e.tipo_pm).IsRequired(false).HasMaxLength(10);
            builder.Property(e => e.estado_ejecucion).IsRequired().HasMaxLength(15).HasDefaultValue("PENDIENTE");
            builder.Property(e => e.observacion_tecnica).IsRequired(false).HasMaxLength(500);

            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany(o => o.Actividades)
                .HasForeignKey(e => e.id_ot)
                .OnDelete(DeleteBehavior.Cascade);

            // id_actividad_ref es solo una referencia suave (no FK de BD)
            // para evitar que cambios en ActividadSistema afecten snapshots históricos
        }
    }
}
