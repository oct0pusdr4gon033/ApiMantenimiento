using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class ControlDocumentarioEquipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expediente",
                columns: table => new
                {
                    codigo_exp = table.Column<string>(type: "varchar(20)", nullable: false),
                    id_equipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expediente", x => x.codigo_exp);
                    table.ForeignKey(
                        name: "FK_Expediente_Equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "Equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    cod_tipo_documento = table.Column<string>(type: "varchar(10)", nullable: false),
                    nombre_tipo = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.cod_tipo_documento);
                });

            migrationBuilder.CreateTable(
                name: "ExpedienteDocumento",
                columns: table => new
                {
                    id_expediente_documento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo_exp = table.Column<string>(type: "varchar(20)", nullable: false),
                    cod_tipo_documento = table.Column<string>(type: "varchar(10)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    documento_url = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteDocumento", x => x.id_expediente_documento);
                    table.ForeignKey(
                        name: "FK_ExpedienteDocumento_Expediente_codigo_exp",
                        column: x => x.codigo_exp,
                        principalTable: "Expediente",
                        principalColumn: "codigo_exp",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpedienteDocumento_TipoDocumento_cod_tipo_documento",
                        column: x => x.cod_tipo_documento,
                        principalTable: "TipoDocumento",
                        principalColumn: "cod_tipo_documento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expediente_id_equipo",
                table: "Expediente",
                column: "id_equipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDocumento_cod_tipo_documento",
                table: "ExpedienteDocumento",
                column: "cod_tipo_documento");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDocumento_codigo_exp",
                table: "ExpedienteDocumento",
                column: "codigo_exp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpedienteDocumento");

            migrationBuilder.DropTable(
                name: "Expediente");

            migrationBuilder.DropTable(
                name: "TipoDocumento");
        }
    }
}
