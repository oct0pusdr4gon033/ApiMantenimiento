using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class FlotaConfiguration: IEntityTypeConfiguration<Flota>
    {
        public void Configure(EntityTypeBuilder<Flota> builder )
        {
            builder.ToTable("Flota");
            builder.HasKey(x=>x.id_flota);
            builder.Property(x=>x.id_modelo).IsRequired(true);

            // CONFIGURACIÓN DE LA RELACIÓN (Flota -> Modelo)
            builder.HasOne(x => x.ModeloEquipo)       // Esta flota tiene UN modelo
                   .WithMany(m => m.Flotas)           // Ese modelo agrupa MUCHAS flotas
                   .HasForeignKey(x => x.id_modelo)   // El campo que los une
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x=>x.nombre_flota).IsRequired(true)
                .HasMaxLength(100);
            builder.Property(x=>x.tipo_control).HasColumnType("varchar(100)");

        }
    }
}
