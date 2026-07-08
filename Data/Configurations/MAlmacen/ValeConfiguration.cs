using ApiMantenimiento.Models.Entitys.MAlmacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MAlmacen
{
    public class ValeConfiguration : IEntityTypeConfiguration<Vale>
    {
        public void Configure(EntityTypeBuilder<Vale> builder)
        {
            builder.ToTable("Alm_Vale");
            builder.HasKey(e => e.id_vale);
            builder.Property(e => e.id_vale).ValueGeneratedOnAdd();

            builder.Property(e => e.cod_vale).IsRequired().HasMaxLength(50);
            builder.HasIndex(e => e.cod_vale).IsUnique();

            builder.Property(e => e.estado).IsRequired().HasMaxLength(20);
            builder.Property(e => e.solicitado_por).IsRequired().HasMaxLength(150);
            builder.Property(e => e.despachado_por).HasMaxLength(150);
            builder.Property(e => e.observaciones).HasMaxLength(500);

            // Relación 1-1 opcional con OrdenTrabajo
            builder.HasOne(e => e.OrdenTrabajo)
                .WithOne(o => o.Vale)
                .HasForeignKey<Vale>(e => e.id_ot)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
