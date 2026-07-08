using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class ExpedienteDocumentoConfiguration : IEntityTypeConfiguration<ExpedienteDocumento>
    {
        public void Configure(EntityTypeBuilder<ExpedienteDocumento> builder)
        {
            builder.ToTable("ExpedienteDocumento");

            // Llave primaria
            builder.HasKey(x => x.id_expediente_documento);

            // Las relaciones están configuradas en ExpedienteConfiguration y TipoDocumentoConfiguration,
            // pero podemos asegurar los requerimientos de las claves foráneas
            builder.Property(x => x.codigo_exp).IsRequired();
            builder.Property(x => x.cod_tipo_documento).IsRequired();

            builder.Property(x => x.fecha_registro).HasColumnType("datetime");
            builder.Property(x => x.fecha_vencimiento).HasColumnType("datetime");
            builder.Property(x => x.documento_url).HasColumnType("varchar(255)");
        }
    }
}
