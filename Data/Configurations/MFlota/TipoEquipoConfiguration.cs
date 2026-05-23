using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class TipoEquipoConfiguration : IEntityTypeConfiguration<TipoEquipo>
    {
        public void Configure(EntityTypeBuilder<TipoEquipo> builder)
        {
            builder.HasKey(x=>x.id_tipo_eqp);
            builder.Property(x => x.cod_equipo).IsRequired(true).HasColumnType("char(10)");
            builder.Property(x => x.nombre_tipo).HasColumnType("varchar(100)");   
        }
    }
}
