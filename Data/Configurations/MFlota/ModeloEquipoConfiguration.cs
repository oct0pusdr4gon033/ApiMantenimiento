using Microsoft.EntityFrameworkCore;
using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class ModeloEquipoConfiguration: IEntityTypeConfiguration<ModeloEquipo>
    {
        public void Configure(EntityTypeBuilder<ModeloEquipo> builder)
        {
            builder.ToTable("ModeloEquipo");

            // Llave Primaria (PK)
            builder.HasKey(x => x.id_modelo);

            // CONFIGURACIÓN DE LA RELACIÓN Y LLAVE FORÁNEA (FK)
            builder.HasOne(x => x.Marca)           // Un Modelo tiene UNA Marca
                   .WithMany(m => m.Modelos)       // Esa Marca tiene MUCHOS Modelos
                   .HasForeignKey(x => x.id_marca) // El campo que los une es "id_marca"
                   .OnDelete(DeleteBehavior.Restrict); // (Opcional) Evita que borres una marca si tiene modelos

            // CONFIGURACIÓN DE LA RELACIÓN Y LLAVE FORÁNEA (FK)
            builder.HasOne(x => x.TipoEquipo)         
                   .WithMany(m => m.ModeloEquipos)       
                   .HasForeignKey(x => x.id_tipo_eqp) 
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.nomb_modelo).HasColumnName("nombre_modelo").HasMaxLength(200); 
        }
    }
    
}
