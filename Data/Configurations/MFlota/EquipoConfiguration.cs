using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class EquipoConfiguration: IEntityTypeConfiguration<Equipo>
    {
        public void Configure(EntityTypeBuilder<Equipo>builder )
        {
            builder.ToTable("Equipo");

            // Llave primaria
            builder.HasKey(x => x.id_equipo);

            // Mapeo específico de tipos de datos SQL
            builder.Property(x => x.cod_eqp).HasColumnType("char(18)");
            builder.Property(x => x.placa_eqp).HasColumnType("varchar(10)");
            builder.Property(x => x.num_serie).HasColumnType("varchar(100)");

            // Mapeo del decimal (10 enteros, 2 decimales)
            builder.Property(x => x.horometro_inicial).HasColumnType("decimal(10,2)");

            builder.Property(x => x.horometro_actual).HasColumnType("decimal(10,2)");

            // cod_are_ope es NOT NULL y varchar(10)
            builder.Property(x => x.cod_are_ope)
                   .HasColumnType("varchar(10)")
                   .IsRequired(); // Esto equivale al NOT NULL de tu SQL
                                  // Conectar Equipo con AreaOperacion
            builder.HasOne(x => x.AreaOperacion)
                   .WithMany(a => a.Equipos)
                   .HasForeignKey(x => x.cod_are_ope) // Aquí le dices: "¡Usa mi columna, no inventes una!"
                   .OnDelete(DeleteBehavior.Restrict);
            // CONFIGURACIÓN DE LA RELACIÓN (1 a Muchos)
            builder.HasOne(x => x.Flota)
                   .WithMany(f => f.Equipos)
                   .HasForeignKey(x => x.id_flota)
                   .OnDelete(DeleteBehavior.Restrict);

            // CONFIGURACIÓN DE LA RELACIÓN (1 a 1)
            builder.HasOne(x => x.Expediente)
                   .WithOne(e => e.Equipo)
                   .HasForeignKey<Expediente>(e => e.id_equipo)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}
