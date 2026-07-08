using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AddConfiguracionOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tipo_pm",
                table: "Man_OTActividad",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "observacion_tecnica",
                table: "Man_OTActividad",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "cod_sistema",
                table: "Man_OTActividad",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "descripcion_falla",
                table: "Man_OrdenTrabajo",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "hora_intervencion",
                table: "Man_OrdenTrabajo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "horometro_falla",
                table: "Man_OrdenTrabajo",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_sistema",
                table: "Man_OrdenTrabajo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_subsistema",
                table: "Man_OrdenTrabajo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "subsistema_equipo",
                columns: table => new
                {
                    id_subsistema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_subsist = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nombre_subsist = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    id_sistema = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subsistema_equipo", x => x.id_subsistema);
                    table.ForeignKey(
                        name: "FK_subsistema_equipo_sistema_equipo_id_sistema",
                        column: x => x.id_sistema,
                        principalTable: "sistema_equipo",
                        principalColumn: "id_sistema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Man_OrdenTrabajo_id_sistema",
                table: "Man_OrdenTrabajo",
                column: "id_sistema");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OrdenTrabajo_id_subsistema",
                table: "Man_OrdenTrabajo",
                column: "id_subsistema");

            migrationBuilder.CreateIndex(
                name: "IX_subsistema_equipo_id_sistema",
                table: "subsistema_equipo",
                column: "id_sistema");

            migrationBuilder.AddForeignKey(
                name: "FK_Man_OrdenTrabajo_sistema_equipo_id_sistema",
                table: "Man_OrdenTrabajo",
                column: "id_sistema",
                principalTable: "sistema_equipo",
                principalColumn: "id_sistema",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Man_OrdenTrabajo_subsistema_equipo_id_subsistema",
                table: "Man_OrdenTrabajo",
                column: "id_subsistema",
                principalTable: "subsistema_equipo",
                principalColumn: "id_subsistema",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Man_OrdenTrabajo_sistema_equipo_id_sistema",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropForeignKey(
                name: "FK_Man_OrdenTrabajo_subsistema_equipo_id_subsistema",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropTable(
                name: "subsistema_equipo");

            migrationBuilder.DropIndex(
                name: "IX_Man_OrdenTrabajo_id_sistema",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_Man_OrdenTrabajo_id_subsistema",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropColumn(
                name: "descripcion_falla",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropColumn(
                name: "hora_intervencion",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropColumn(
                name: "horometro_falla",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropColumn(
                name: "id_sistema",
                table: "Man_OrdenTrabajo");

            migrationBuilder.DropColumn(
                name: "id_subsistema",
                table: "Man_OrdenTrabajo");

            migrationBuilder.AlterColumn<string>(
                name: "tipo_pm",
                table: "Man_OTActividad",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observacion_tecnica",
                table: "Man_OTActividad",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cod_sistema",
                table: "Man_OTActividad",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
