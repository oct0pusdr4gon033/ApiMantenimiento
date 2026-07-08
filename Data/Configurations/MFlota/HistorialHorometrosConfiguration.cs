using ApiMantenimiento.Models.Entitys.MFlota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MFlota
{
    public class HistorialHorometrosConfiguration : IEntityTypeConfiguration<HistorialHorometros>
    {
        public void Configure(EntityTypeBuilder<HistorialHorometros> builder)
        {
            builder.ToTable("HistorialHorometros");

            // Llave primaria
            builder.HasKey(x => x.codigo_hist);

            // Tipos de datos SQL
            builder.Property(x => x.codigo_hist).HasColumnType("varchar(20)");
            builder.Property(x => x.dni_conductor).HasColumnType("char(8)");
            builder.Property(x => x.observaciones).HasColumnType("varchar(255)");

            builder.Property(x => x.lectura_anterior).HasColumnType("decimal(10,2)");
            builder.Property(x => x.lectura_actual).HasColumnType("decimal(10,2)");
            builder.Property(x => x.horas_operadas).HasColumnType("decimal(10,2)");

            // Configuración de la relación con Equipo (si existiera en EF)
            builder.HasOne<Equipo>()
                   .WithMany()
                   .HasForeignKey(x => x.id_equipo)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación con Empleado
            builder.HasOne(x => x.Empleado)
                   .WithMany()
                   .HasForeignKey(x => x.dni_conductor)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
