using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class Empleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialHorometros",
                columns: table => new
                {
                    codigo_hist = table.Column<string>(type: "varchar(20)", nullable: false),
                    dni_conductor = table.Column<string>(type: "char(8)", nullable: false),
                    id_equipo = table.Column<int>(type: "int", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lectura_anterior = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    lectura_actual = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    horas_operadas = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    observaciones = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialHorometros", x => x.codigo_hist);
                    table.ForeignKey(
                        name: "FK_HistorialHorometros_Equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "Equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_rol = table.Column<string>(type: "varchar(50)", nullable: false),
                    prefijo = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    dni_empleado = table.Column<string>(type: "varchar(15)", nullable: false),
                    codigo_empleado = table.Column<string>(type: "varchar(20)", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    apellido1 = table.Column<string>(type: "varchar(100)", nullable: false),
                    apellido2 = table.Column<string>(type: "varchar(100)", nullable: false),
                    telf = table.Column<string>(type: "varchar(20)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.dni_empleado);
                    table.ForeignKey(
                        name: "FK_Empleado_Rol_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_id_rol",
                table: "Empleado",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialHorometros_id_equipo",
                table: "HistorialHorometros",
                column: "id_equipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "HistorialHorometros");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
