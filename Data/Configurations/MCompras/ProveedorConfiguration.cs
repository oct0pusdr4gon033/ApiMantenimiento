using ApiMantenimiento.Models.Entitys.MCompras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMantenimiento.Data.Configurations.MCompras
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Com_Proveedor");
            builder.HasKey(e => e.ruc);
            builder.Property(e => e.ruc).HasColumnType("varchar(20)").IsRequired();
            builder.Property(e => e.razon_social).HasColumnType("varchar(250)").IsRequired();
            builder.Property(e => e.nombre_comercial).HasColumnType("varchar(250)");
            builder.Property(e => e.direccion).HasColumnType("varchar(500)");
            builder.Property(e => e.correo).HasColumnType("varchar(150)");
            builder.Property(e => e.telefono).HasColumnType("varchar(50)");
            builder.Property(e => e.estado).HasColumnType("varchar(20)").HasDefaultValue("ACTIVO");

            // Seeding 10 Suppliers
            builder.HasData(
                new Proveedor
                {
                    ruc = "20502123456",
                    razon_social = "Sodimac Perú S.A.",
                    nombre_comercial = "Sodimac",
                    direccion = "Av. Salaverry 2450, San Isidro",
                    correo = "contacto@sodimac.com.pe",
                    telefono = "(01) 615-6000",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20384729104",
                    razon_social = "Maestro Perú S.A.",
                    nombre_comercial = "Maestro",
                    direccion = "Av. Paseo de la República 3200, San Isidro",
                    correo = "ventas@maestro.com.pe",
                    telefono = "(01) 619-3000",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20543297120",
                    razon_social = "Homecenters Peruanos S.A.",
                    nombre_comercial = "Promart",
                    direccion = "Av. Aviación 2405, San Borja",
                    correo = "ventas@promart.pe",
                    telefono = "(01) 619-4000",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20100027225",
                    razon_social = "Ferreyros S.A.",
                    nombre_comercial = "Ferreyros CAT",
                    direccion = "Jr. Cristobal de Peralta Norte 820, Santiago de Surco",
                    correo = "contacto@ferreyros.com.pe",
                    telefono = "(01) 626-4000",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20100013003",
                    razon_social = "Corporación Aceros Arequipa S.A.",
                    nombre_comercial = "Aceros Arequipa",
                    direccion = "Av. Enrique Meiggs 270, Parque Industrial, Callao",
                    correo = "atencion@acerosarequipa.com",
                    telefono = "(01) 517-1800",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20100057051",
                    razon_social = "Empresa Siderúrgica del Perú S.A.A.",
                    nombre_comercial = "Siderperu",
                    direccion = "Av. Juan de Arona 151, San Isidro",
                    correo = "comercial@sider.com.pe",
                    telefono = "(01) 618-6000",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20601234567",
                    razon_social = "Constructora e Inversiones Globales S.A.C.",
                    nombre_comercial = "Inversiones Globales",
                    direccion = "Av. Javier Prado Este 1234, La Molina",
                    correo = "administracion@globales.pe",
                    telefono = "987654321",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20512837490",
                    razon_social = "Distribuidora Eléctrica del Perú S.A.",
                    nombre_comercial = "ElectroPerú Distribuciones",
                    direccion = "Av. Argentina 4500, Callao",
                    correo = "informes@electroperu-dist.com",
                    telefono = "(01) 452-9080",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20100021340",
                    razon_social = "Importaciones Hiraoka S.A.C.",
                    nombre_comercial = "Hiraoka",
                    direccion = "Av. Abancay 594, Lima",
                    correo = "ventas_web@hiraoka.com.pe",
                    telefono = "(01) 311-8200",
                    estado = "ACTIVO"
                },
                new Proveedor
                {
                    ruc = "20123456789",
                    razon_social = "Asociación Tecsup Nro 2",
                    nombre_comercial = "Tecsup",
                    direccion = "Av. Cascanueces 2221, Santa Anita",
                    correo = "capacitacion@tecsup.edu.pe",
                    telefono = "(01) 317-3900",
                    estado = "ACTIVO"
                }
            );
        }
    }
}
