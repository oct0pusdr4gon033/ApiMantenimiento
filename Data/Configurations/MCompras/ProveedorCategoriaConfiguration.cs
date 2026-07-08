using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class ProveedorCategoriaConfiguration : IEntityTypeConfiguration<ProveedorCategoria>
    {
        public void Configure(EntityTypeBuilder<ProveedorCategoria> builder)
        {
            builder.ToTable("Com_ProveedorCategoria");
            builder.HasKey(e => new { e.ruc, e.cod_cat });

            builder.Property(e => e.ruc).HasColumnType("varchar(20)").IsRequired();
            builder.Property(e => e.cod_cat).HasColumnType("varchar(50)").IsRequired();

            builder.HasOne(e => e.Proveedor)
                .WithMany(p => p.ProveedorCategorias)
                .HasForeignKey(e => e.ruc)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.CategoriaProveedor)
                .WithMany(c => c.ProveedorCategorias)
                .HasForeignKey(e => e.cod_cat)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed some relations
            builder.HasData(
                // Sodimac (20502123456): Ferreteria, Hogar, Cultivo
                new ProveedorCategoria { ruc = "20502123456", cod_cat = "FERRETERIA" },
                new ProveedorCategoria { ruc = "20502123456", cod_cat = "HOGAR" },
                new ProveedorCategoria { ruc = "20502123456", cod_cat = "CULTIVO" },
                
                // Maestro (20384729104): Ferreteria, Construccion
                new ProveedorCategoria { ruc = "20384729104", cod_cat = "FERRETERIA" },
                new ProveedorCategoria { ruc = "20384729104", cod_cat = "CONSTRUCCION" },

                // Promart (20543297120): Hogar, Ferreteria
                new ProveedorCategoria { ruc = "20543297120", cod_cat = "HOGAR" },
                new ProveedorCategoria { ruc = "20543297120", cod_cat = "FERRETERIA" },

                // Ferreyros (20100027225): Servicios, Construccion
                new ProveedorCategoria { ruc = "20100027225", cod_cat = "SERVICIOS" },
                new ProveedorCategoria { ruc = "20100027225", cod_cat = "CONSTRUCCION" },

                // Aceros Arequipa (20100013003): Construccion
                new ProveedorCategoria { ruc = "20100013003", cod_cat = "CONSTRUCCION" },

                // Siderperu (20100057051): Construccion
                new ProveedorCategoria { ruc = "20100057051", cod_cat = "CONSTRUCCION" },

                // Distribuidora Electrica (20512837490): Electricidad
                new ProveedorCategoria { ruc = "20512837490", cod_cat = "ELECTRICIDAD" }
            );
        }
    }
}
