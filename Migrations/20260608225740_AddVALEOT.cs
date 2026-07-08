using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AddVALEOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alm_MovimientoInventario",
                columns: table => new
                {
                    id_movimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo_movimiento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saldo_stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    origen_tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    origen_referencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    responsable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_MovimientoInventario", x => x.id_movimiento);
                    table.ForeignKey(
                        name: "FK_Alm_MovimientoInventario_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alm_Vale",
                columns: table => new
                {
                    id_vale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_vale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_ot = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_despacho = table.Column<DateTime>(type: "datetime2", nullable: true),
                    solicitado_por = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    despachado_por = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_Vale", x => x.id_vale);
                    table.ForeignKey(
                        name: "FK_Alm_Vale_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Alm_ValeMaterial",
                columns: table => new
                {
                    id_vale_material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_vale = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    cantidad_solicitada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cantidad_despachada = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_ValeMaterial", x => x.id_vale_material);
                    table.ForeignKey(
                        name: "FK_Alm_ValeMaterial_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alm_ValeMaterial_Alm_Vale_id_vale",
                        column: x => x.id_vale,
                        principalTable: "Alm_Vale",
                        principalColumn: "id_vale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alm_MovimientoInventario_id_material",
                table: "Alm_MovimientoInventario",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Alm_Vale_cod_vale",
                table: "Alm_Vale",
                column: "cod_vale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alm_Vale_id_ot",
                table: "Alm_Vale",
                column: "id_ot",
                unique: true,
                filter: "[id_ot] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Alm_ValeMaterial_id_material",
                table: "Alm_ValeMaterial",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Alm_ValeMaterial_id_vale",
                table: "Alm_ValeMaterial",
                column: "id_vale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alm_MovimientoInventario");

            migrationBuilder.DropTable(
                name: "Alm_ValeMaterial");

            migrationBuilder.DropTable(
                name: "Alm_Vale");
        }
    }
}
