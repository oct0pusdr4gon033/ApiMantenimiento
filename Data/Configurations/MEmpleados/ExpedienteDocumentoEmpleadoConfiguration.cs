using ApiMantenimiento.Models.Entitys.MEmpleados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MEmpleados
{
    public class ExpedienteDocumentoEmpleadoConfiguration : IEntityTypeConfiguration<ExpedienteDocumentoEmpleado>
    {
        public void Configure(EntityTypeBuilder<ExpedienteDocumentoEmpleado> builder)
        {
            builder.ToTable("ExpedienteDocumentoEmpleado");

            // Llave primaria
            builder.HasKey(x => x.id_exp_doc_emp);

            // Las relaciones están configuradas en ExpedienteEmpleadoConfiguration y TipoDocumentoEmpleadoConfiguration,
            // pero podemos asegurar los requerimientos de las claves foráneas
            builder.Property(x => x.codigo_exp_emp).IsRequired();
            builder.Property(x => x.cod_tipo_doc_emp).IsRequired();

            builder.Property(x => x.fecha_registro).HasColumnType("datetime");
            builder.Property(x => x.fecha_vencimiento).HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.documento_url).HasColumnType("varchar(255)");
        }
    }
}
