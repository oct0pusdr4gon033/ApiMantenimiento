using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class Empleado2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.AlterColumn<string>(
                name: "dni_empleado",
                table: "Empleado",
                type: "char(8)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "dni_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialHorometros_dni_conductor",
                table: "HistorialHorometros",
                column: "dni_conductor");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialHorometros_Empleado_dni_conductor",
                table: "HistorialHorometros",
                column: "dni_conductor",
                principalTable: "Empleado",
                principalColumn: "dni_empleado",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialHorometros_Empleado_dni_conductor",
                table: "HistorialHorometros");

            migrationBuilder.DropIndex(
                name: "IX_HistorialHorometros_dni_conductor",
                table: "HistorialHorometros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.AlterColumn<string>(
                name: "dni_empleado",
                table: "Empleado",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(8)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "dni_empleado");
        }
    }
}
