using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("TipoDocumento");

            // Llave primaria
            builder.HasKey(x => x.cod_tipo_documento);

            // Propiedades
            builder.Property(x => x.cod_tipo_documento)
                   .HasColumnType("varchar(10)");

            builder.Property(x => x.nombre_tipo)
                   .HasColumnType("varchar(100)")
                   .IsRequired();

            // Relación 1 a Muchos con ExpedienteDocumento
            builder.HasMany(x => x.DetallesDocumento)
                   .WithOne(ed => ed.TipoDocumento)
                   .HasForeignKey(ed => ed.cod_tipo_documento)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
