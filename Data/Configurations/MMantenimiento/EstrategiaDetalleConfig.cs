using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiMantenimiento.Models.Entitys.MMantenimiento;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class EstrategiaDetalleConfig : IEntityTypeConfiguration<EstrategiaDetalle>
    {
        public void Configure(EntityTypeBuilder<EstrategiaDetalle> builder)
        {
            builder.ToTable("EstrategiaDetalle");

            builder.HasKey(ed => ed.id_detalle_estrg);

            builder.Property(ed => ed.id_detalle_estrg)
                   .ValueGeneratedOnAdd();

            builder.Property(ed => ed.nombre_medida)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(ed => ed.uni_med)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(ed => ed.tipo_pm)
                   .HasMaxLength(50);

            builder.Property(ed => ed.umbral_mant).HasColumnType("decimal(18,2)");
            builder.Property(ed => ed.tolerancia_inf).HasColumnType("decimal(18,2)");
            builder.Property(ed => ed.tolerancia_sup).HasColumnType("decimal(18,2)");
            builder.Property(ed => ed.porcentaje_tol).HasColumnType("decimal(18,2)");

            // Relación 1 a muchos: Estrategia -> EstrategiaDetalle
            builder.HasOne(ed => ed.Estrategia)
                   .WithMany(e => e.Detalles)
                   .HasForeignKey(ed => ed.id_estrategia)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
