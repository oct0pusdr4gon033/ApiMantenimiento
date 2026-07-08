using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class ModuloCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "precio_actual",
                table: "Alm_Material",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "stock_minimo",
                table: "Alm_Material",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Alm_HistorialPrecio",
                columns: table => new
                {
                    id_historial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_HistorialPrecio", x => x.id_historial);
                    table.ForeignKey(
                        name: "FK_Alm_HistorialPrecio_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Com_CategoriaProveedor",
                columns: table => new
                {
                    cod_cat = table.Column<string>(type: "varchar(50)", nullable: false),
                    nombre_cat = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_CategoriaProveedor", x => x.cod_cat);
                });

            migrationBuilder.CreateTable(
                name: "Com_Proveedor",
                columns: table => new
                {
                    ruc = table.Column<string>(type: "varchar(20)", nullable: false),
                    razon_social = table.Column<string>(type: "varchar(250)", nullable: false),
                    nombre_comercial = table.Column<string>(type: "varchar(250)", nullable: false),
                    direccion = table.Column<string>(type: "varchar(500)", nullable: false),
                    correo = table.Column<string>(type: "varchar(150)", nullable: false),
                    telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "ACTIVO")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_Proveedor", x => x.ruc);
                });

            migrationBuilder.CreateTable(
                name: "Com_SolicitudPedido",
                columns: table => new
                {
                    id_solicitud_pedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_solicitud = table.Column<string>(type: "varchar(50)", nullable: false),
                    dni_empleado = table.Column<string>(type: "char(8)", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "PENDIENTE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_SolicitudPedido", x => x.id_solicitud_pedido);
                    table.ForeignKey(
                        name: "FK_Com_SolicitudPedido_Empleado_dni_empleado",
                        column: x => x.dni_empleado,
                        principalTable: "Empleado",
                        principalColumn: "dni_empleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Com_ProveedorCategoria",
                columns: table => new
                {
                    ruc = table.Column<string>(type: "varchar(20)", nullable: false),
                    cod_cat = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_ProveedorCategoria", x => new { x.ruc, x.cod_cat });
                    table.ForeignKey(
                        name: "FK_Com_ProveedorCategoria_Com_CategoriaProveedor_cod_cat",
                        column: x => x.cod_cat,
                        principalTable: "Com_CategoriaProveedor",
                        principalColumn: "cod_cat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Com_ProveedorCategoria_Com_Proveedor_ruc",
                        column: x => x.ruc,
                        principalTable: "Com_Proveedor",
                        principalColumn: "ruc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Com_ProveedorContacto",
                columns: table => new
                {
                    id_contacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ruc_proveedor = table.Column<string>(type: "varchar(20)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    apellido1 = table.Column<string>(type: "varchar(100)", nullable: false),
                    apellido2 = table.Column<string>(type: "varchar(100)", nullable: false),
                    correo = table.Column<string>(type: "varchar(150)", nullable: false),
                    telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "ACTIVO")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_ProveedorContacto", x => x.id_contacto);
                    table.ForeignKey(
                        name: "FK_Com_ProveedorContacto_Com_Proveedor_ruc_proveedor",
                        column: x => x.ruc_proveedor,
                        principalTable: "Com_Proveedor",
                        principalColumn: "ruc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Com_Cotizacion",
                columns: table => new
                {
                    id_cotizacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nro_cotizacion = table.Column<string>(type: "varchar(50)", nullable: false),
                    id_solicitud_pedido = table.Column<int>(type: "int", nullable: true),
                    ruc_proveedor = table.Column<string>(type: "varchar(20)", nullable: false),
                    fecha_cotizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "PENDIENTE"),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_Cotizacion", x => x.id_cotizacion);
                    table.ForeignKey(
                        name: "FK_Com_Cotizacion_Com_Proveedor_ruc_proveedor",
                        column: x => x.ruc_proveedor,
                        principalTable: "Com_Proveedor",
                        principalColumn: "ruc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_Cotizacion_Com_SolicitudPedido_id_solicitud_pedido",
                        column: x => x.id_solicitud_pedido,
                        principalTable: "Com_SolicitudPedido",
                        principalColumn: "id_solicitud_pedido",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Com_SolicitudPedidoDetalle",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_solicitud_pedido = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: true),
                    cod_materia = table.Column<string>(type: "varchar(50)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(250)", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: true),
                    id_unidad = table.Column<int>(type: "int", nullable: true),
                    stock_minimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    cantidad_pedida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ruc_proveedor = table.Column<string>(type: "varchar(20)", nullable: false),
                    es_nuevo_producto = table.Column<bool>(type: "bit", nullable: false),
                    especificaciones = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_SolicitudPedidoDetalle", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK_Com_SolicitudPedidoDetalle_Alm_CategoriaMaterial_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Alm_CategoriaMaterial",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_SolicitudPedidoDetalle_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_SolicitudPedidoDetalle_Alm_UnidadMedida_id_unidad",
                        column: x => x.id_unidad,
                        principalTable: "Alm_UnidadMedida",
                        principalColumn: "id_unidad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_SolicitudPedidoDetalle_Com_Proveedor_ruc_proveedor",
                        column: x => x.ruc_proveedor,
                        principalTable: "Com_Proveedor",
                        principalColumn: "ruc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_SolicitudPedidoDetalle_Com_SolicitudPedido_id_solicitud_pedido",
                        column: x => x.id_solicitud_pedido,
                        principalTable: "Com_SolicitudPedido",
                        principalColumn: "id_solicitud_pedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Com_CotizacionDetalle",
                columns: table => new
                {
                    id_cotizacion_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cotizacion = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_CotizacionDetalle", x => x.id_cotizacion_detalle);
                    table.ForeignKey(
                        name: "FK_Com_CotizacionDetalle_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_CotizacionDetalle_Com_Cotizacion_id_cotizacion",
                        column: x => x.id_cotizacion,
                        principalTable: "Com_Cotizacion",
                        principalColumn: "id_cotizacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Com_OrdenCompra",
                columns: table => new
                {
                    id_orden_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nro_orden = table.Column<string>(type: "varchar(50)", nullable: false),
                    id_cotizacion = table.Column<int>(type: "int", nullable: true),
                    ruc_proveedor = table.Column<string>(type: "varchar(20)", nullable: false),
                    fecha_orden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "PENDIENTE"),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_OrdenCompra", x => x.id_orden_compra);
                    table.ForeignKey(
                        name: "FK_Com_OrdenCompra_Com_Cotizacion_id_cotizacion",
                        column: x => x.id_cotizacion,
                        principalTable: "Com_Cotizacion",
                        principalColumn: "id_cotizacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_OrdenCompra_Com_Proveedor_ruc_proveedor",
                        column: x => x.ruc_proveedor,
                        principalTable: "Com_Proveedor",
                        principalColumn: "ruc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Com_NotaIngreso",
                columns: table => new
                {
                    id_nota_ingreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nro_nota = table.Column<string>(type: "varchar(50)", nullable: false),
                    id_orden_compra = table.Column<int>(type: "int", nullable: false),
                    fecha_ingreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "PROCESADO"),
                    observaciones = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_NotaIngreso", x => x.id_nota_ingreso);
                    table.ForeignKey(
                        name: "FK_Com_NotaIngreso_Com_OrdenCompra_id_orden_compra",
                        column: x => x.id_orden_compra,
                        principalTable: "Com_OrdenCompra",
                        principalColumn: "id_orden_compra",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Com_OrdenCompraDetalle",
                columns: table => new
                {
                    id_orden_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_orden_compra = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_OrdenCompraDetalle", x => x.id_orden_detalle);
                    table.ForeignKey(
                        name: "FK_Com_OrdenCompraDetalle_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_OrdenCompraDetalle_Com_OrdenCompra_id_orden_compra",
                        column: x => x.id_orden_compra,
                        principalTable: "Com_OrdenCompra",
                        principalColumn: "id_orden_compra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Com_NotaIngresoDetalle",
                columns: table => new
                {
                    id_nota_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nota_ingreso = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Com_NotaIngresoDetalle", x => x.id_nota_detalle);
                    table.ForeignKey(
                        name: "FK_Com_NotaIngresoDetalle_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Com_NotaIngresoDetalle_Com_NotaIngreso_id_nota_ingreso",
                        column: x => x.id_nota_ingreso,
                        principalTable: "Com_NotaIngreso",
                        principalColumn: "id_nota_ingreso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Com_CategoriaProveedor",
                columns: new[] { "cod_cat", "nombre_cat" },
                values: new object[,]
                {
                    { "CONSTRUCCION", "Materiales de Construcción" },
                    { "CULTIVO", "Cultivo y Jardinería" },
                    { "ELECTRICIDAD", "Materiales Eléctricos" },
                    { "FERRETERIA", "Ferretería y Herramientas" },
                    { "HOGAR", "Artículos para el Hogar" },
                    { "SEGURIDAD", "Seguridad e Higiene Industrial" },
                    { "SERVICIOS", "Servicios y Asesorías Técnicas" }
                });

            migrationBuilder.InsertData(
                table: "Com_Proveedor",
                columns: new[] { "ruc", "correo", "direccion", "estado", "nombre_comercial", "razon_social", "telefono" },
                values: new object[,]
                {
                    { "20100013003", "atencion@acerosarequipa.com", "Av. Enrique Meiggs 270, Parque Industrial, Callao", "ACTIVO", "Aceros Arequipa", "Corporación Aceros Arequipa S.A.", "(01) 517-1800" },
                    { "20100021340", "ventas_web@hiraoka.com.pe", "Av. Abancay 594, Lima", "ACTIVO", "Hiraoka", "Importaciones Hiraoka S.A.C.", "(01) 311-8200" },
                    { "20100027225", "contacto@ferreyros.com.pe", "Jr. Cristobal de Peralta Norte 820, Santiago de Surco", "ACTIVO", "Ferreyros CAT", "Ferreyros S.A.", "(01) 626-4000" },
                    { "20100057051", "comercial@sider.com.pe", "Av. Juan de Arona 151, San Isidro", "ACTIVO", "Siderperu", "Empresa Siderúrgica del Perú S.A.A.", "(01) 618-6000" },
                    { "20123456789", "capacitacion@tecsup.edu.pe", "Av. Cascanueces 2221, Santa Anita", "ACTIVO", "Tecsup", "Asociación Tecsup Nro 2", "(01) 317-3900" },
                    { "20384729104", "ventas@maestro.com.pe", "Av. Paseo de la República 3200, San Isidro", "ACTIVO", "Maestro", "Maestro Perú S.A.", "(01) 619-3000" },
                    { "20502123456", "contacto@sodimac.com.pe", "Av. Salaverry 2450, San Isidro", "ACTIVO", "Sodimac", "Sodimac Perú S.A.", "(01) 615-6000" },
                    { "20512837490", "informes@electroperu-dist.com", "Av. Argentina 4500, Callao", "ACTIVO", "ElectroPerú Distribuciones", "Distribuidora Eléctrica del Perú S.A.", "(01) 452-9080" },
                    { "20543297120", "ventas@promart.pe", "Av. Aviación 2405, San Borja", "ACTIVO", "Promart", "Homecenters Peruanos S.A.", "(01) 619-4000" },
                    { "20601234567", "administracion@globales.pe", "Av. Javier Prado Este 1234, La Molina", "ACTIVO", "Inversiones Globales", "Constructora e Inversiones Globales S.A.C.", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Com_ProveedorCategoria",
                columns: new[] { "cod_cat", "ruc" },
                values: new object[,]
                {
                    { "CONSTRUCCION", "20100013003" },
                    { "CONSTRUCCION", "20100027225" },
                    { "SERVICIOS", "20100027225" },
                    { "CONSTRUCCION", "20100057051" },
                    { "CONSTRUCCION", "20384729104" },
                    { "FERRETERIA", "20384729104" },
                    { "CULTIVO", "20502123456" },
                    { "FERRETERIA", "20502123456" },
                    { "HOGAR", "20502123456" },
                    { "ELECTRICIDAD", "20512837490" },
                    { "FERRETERIA", "20543297120" },
                    { "HOGAR", "20543297120" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alm_HistorialPrecio_id_material",
                table: "Alm_HistorialPrecio",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Com_Cotizacion_id_solicitud_pedido",
                table: "Com_Cotizacion",
                column: "id_solicitud_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_Com_Cotizacion_nro_cotizacion",
                table: "Com_Cotizacion",
                column: "nro_cotizacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Com_Cotizacion_ruc_proveedor",
                table: "Com_Cotizacion",
                column: "ruc_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Com_CotizacionDetalle_id_cotizacion",
                table: "Com_CotizacionDetalle",
                column: "id_cotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Com_CotizacionDetalle_id_material",
                table: "Com_CotizacionDetalle",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Com_NotaIngreso_id_orden_compra",
                table: "Com_NotaIngreso",
                column: "id_orden_compra");

            migrationBuilder.CreateIndex(
                name: "IX_Com_NotaIngreso_nro_nota",
                table: "Com_NotaIngreso",
                column: "nro_nota",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Com_NotaIngresoDetalle_id_material",
                table: "Com_NotaIngresoDetalle",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Com_NotaIngresoDetalle_id_nota_ingreso",
                table: "Com_NotaIngresoDetalle",
                column: "id_nota_ingreso");

            migrationBuilder.CreateIndex(
                name: "IX_Com_OrdenCompra_id_cotizacion",
                table: "Com_OrdenCompra",
                column: "id_cotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Com_OrdenCompra_nro_orden",
                table: "Com_OrdenCompra",
                column: "nro_orden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Com_OrdenCompra_ruc_proveedor",
                table: "Com_OrdenCompra",
                column: "ruc_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Com_OrdenCompraDetalle_id_material",
                table: "Com_OrdenCompraDetalle",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Com_OrdenCompraDetalle_id_orden_compra",
                table: "Com_OrdenCompraDetalle",
                column: "id_orden_compra");

            migrationBuilder.CreateIndex(
                name: "IX_Com_ProveedorCategoria_cod_cat",
                table: "Com_ProveedorCategoria",
                column: "cod_cat");

            migrationBuilder.CreateIndex(
                name: "IX_Com_ProveedorContacto_ruc_proveedor",
                table: "Com_ProveedorContacto",
                column: "ruc_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedido_cod_solicitud",
                table: "Com_SolicitudPedido",
                column: "cod_solicitud",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedido_dni_empleado",
                table: "Com_SolicitudPedido",
                column: "dni_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedidoDetalle_id_categoria",
                table: "Com_SolicitudPedidoDetalle",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedidoDetalle_id_material",
                table: "Com_SolicitudPedidoDetalle",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedidoDetalle_id_solicitud_pedido",
                table: "Com_SolicitudPedidoDetalle",
                column: "id_solicitud_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedidoDetalle_id_unidad",
                table: "Com_SolicitudPedidoDetalle",
                column: "id_unidad");

            migrationBuilder.CreateIndex(
                name: "IX_Com_SolicitudPedidoDetalle_ruc_proveedor",
                table: "Com_SolicitudPedidoDetalle",
                column: "ruc_proveedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alm_HistorialPrecio");

            migrationBuilder.DropTable(
                name: "Com_CotizacionDetalle");

            migrationBuilder.DropTable(
                name: "Com_NotaIngresoDetalle");

            migrationBuilder.DropTable(
                name: "Com_OrdenCompraDetalle");

            migrationBuilder.DropTable(
                name: "Com_ProveedorCategoria");

            migrationBuilder.DropTable(
                name: "Com_ProveedorContacto");

            migrationBuilder.DropTable(
                name: "Com_SolicitudPedidoDetalle");

            migrationBuilder.DropTable(
                name: "Com_NotaIngreso");

            migrationBuilder.DropTable(
                name: "Com_CategoriaProveedor");

            migrationBuilder.DropTable(
                name: "Com_OrdenCompra");

            migrationBuilder.DropTable(
                name: "Com_Cotizacion");

            migrationBuilder.DropTable(
                name: "Com_Proveedor");

            migrationBuilder.DropTable(
                name: "Com_SolicitudPedido");

            migrationBuilder.DropColumn(
                name: "precio_actual",
                table: "Alm_Material");

            migrationBuilder.DropColumn(
                name: "stock_minimo",
                table: "Alm_Material");
        }
    }
}
