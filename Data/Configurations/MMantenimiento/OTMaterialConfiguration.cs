using ApiMantenimiento.Models.Entitys.MMantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MMantenimiento
{
    public class OTMaterialConfiguration : IEntityTypeConfiguration<OTMaterial>
    {
        public void Configure(EntityTypeBuilder<OTMaterial> builder)
        {
            builder.ToTable("Man_OTMaterial", tb => tb.HasTrigger("TR_Man_OTMaterial_AfterInsert"));
            builder.HasKey(e => e.id_ot_material);

            builder.Property(e => e.cod_materia).HasMaxLength(30);
            builder.Property(e => e.descripcion_material).IsRequired().HasMaxLength(200);
            builder.Property(e => e.cantidad_requerida).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.cantidad_utilizada).HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany(o => o.Materiales)
                .HasForeignKey(e => e.id_ot)
                .OnDelete(DeleteBehavior.Cascade);

            // id_material_ref es referencia suave — no FK de BD para preservar historial
        }
    }
}
