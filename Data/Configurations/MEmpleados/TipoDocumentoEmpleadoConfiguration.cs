using ApiMantenimiento.Models.Entitys.MEmpleados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MEmpleados
{
    public class TipoDocumentoEmpleadoConfiguration : IEntityTypeConfiguration<TipoDocumentoEmpleado>
    {
        public void Configure(EntityTypeBuilder<TipoDocumentoEmpleado> builder)
        {
            builder.ToTable("TipoDocumentoEmpleado");

            // Llave primaria
            builder.HasKey(x => x.cod_tipo_doc_emp);

            // Propiedades
            builder.Property(x => x.cod_tipo_doc_emp)
                   .HasColumnType("varchar(10)");

            builder.Property(x => x.nombre_tipo)
                   .HasColumnType("varchar(100)")
                   .IsRequired();

            // Relación 1 a Muchos con ExpedienteDocumentoEmpleado
            builder.HasMany(x => x.DetallesDocumento)
                   .WithOne(ed => ed.TipoDocumentoEmpleado)
                   .HasForeignKey(ed => ed.cod_tipo_doc_emp)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
