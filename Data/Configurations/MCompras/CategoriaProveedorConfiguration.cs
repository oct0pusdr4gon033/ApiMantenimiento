using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class CategoriaProveedorConfiguration : IEntityTypeConfiguration<CategoriaProveedor>
    {
        public void Configure(EntityTypeBuilder<CategoriaProveedor> builder)
        {
            builder.ToTable("Com_CategoriaProveedor");
            builder.HasKey(e => e.cod_cat);
            builder.Property(e => e.cod_cat).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.nombre_cat).HasColumnType("varchar(150)").IsRequired();

            // Seed categories
            builder.HasData(
                new CategoriaProveedor { cod_cat = "FERRETERIA", nombre_cat = "Ferretería y Herramientas" },
                new CategoriaProveedor { cod_cat = "HOGAR", nombre_cat = "Artículos para el Hogar" },
                new CategoriaProveedor { cod_cat = "CULTIVO", nombre_cat = "Cultivo y Jardinería" },
                new CategoriaProveedor { cod_cat = "ELECTRICIDAD", nombre_cat = "Materiales Eléctricos" },
                new CategoriaProveedor { cod_cat = "CONSTRUCCION", nombre_cat = "Materiales de Construcción" },
                new CategoriaProveedor { cod_cat = "SEGURIDAD", nombre_cat = "Seguridad e Higiene Industrial" },
                new CategoriaProveedor { cod_cat = "SERVICIOS", nombre_cat = "Servicios y Asesorías Técnicas" }
            );
        }
    }
}
