using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AddEstrategiaMantenimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "ExpedienteDocumentoEmpleado",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateTable(
                name: "Estrategia",
                columns: table => new
                {
                    id_estrategia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_estrategia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    titulo_estrategia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_flota = table.Column<int>(type: "int", nullable: true),
                    id_equipo = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estrategia", x => x.id_estrategia);
                    table.ForeignKey(
                        name: "FK_Estrategia_Equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "Equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estrategia_Flota_id_flota",
                        column: x => x.id_flota,
                        principalTable: "Flota",
                        principalColumn: "id_flota",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstrategiaDetalle",
                columns: table => new
                {
                    id_detalle_estrg = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_estrategia = table.Column<int>(type: "int", nullable: false),
                    umbral_mant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tolerancia_inf = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tolerancia_sup = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    porcentaje_tol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nombre_medida = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    uni_med = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstrategiaDetalle", x => x.id_detalle_estrg);
                    table.ForeignKey(
                        name: "FK_EstrategiaDetalle_Estrategia_id_estrategia",
                        column: x => x.id_estrategia,
                        principalTable: "Estrategia",
                        principalColumn: "id_estrategia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estrategia_id_equipo",
                table: "Estrategia",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "IX_Estrategia_id_flota",
                table: "Estrategia",
                column: "id_flota");

            migrationBuilder.CreateIndex(
                name: "IX_EstrategiaDetalle_id_estrategia",
                table: "EstrategiaDetalle",
                column: "id_estrategia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstrategiaDetalle");

            migrationBuilder.DropTable(
                name: "Estrategia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "ExpedienteDocumentoEmpleado",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
