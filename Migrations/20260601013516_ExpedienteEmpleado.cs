using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class ExpedienteEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpedienteEmpleado",
                columns: table => new
                {
                    codigo_exp_emp = table.Column<string>(type: "varchar(20)", nullable: false),
                    dni_empleado = table.Column<string>(type: "char(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteEmpleado", x => x.codigo_exp_emp);
                    table.ForeignKey(
                        name: "FK_ExpedienteEmpleado_Empleado_dni_empleado",
                        column: x => x.dni_empleado,
                        principalTable: "Empleado",
                        principalColumn: "dni_empleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentoEmpleado",
                columns: table => new
                {
                    cod_tipo_doc_emp = table.Column<string>(type: "varchar(10)", nullable: false),
                    nombre_tipo = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentoEmpleado", x => x.cod_tipo_doc_emp);
                });

            migrationBuilder.CreateTable(
                name: "ExpedienteDocumentoEmpleado",
                columns: table => new
                {
                    id_exp_doc_emp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo_exp_emp = table.Column<string>(type: "varchar(20)", nullable: false),
                    cod_tipo_doc_emp = table.Column<string>(type: "varchar(10)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    documento_url = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteDocumentoEmpleado", x => x.id_exp_doc_emp);
                    table.ForeignKey(
                        name: "FK_ExpedienteDocumentoEmpleado_ExpedienteEmpleado_codigo_exp_emp",
                        column: x => x.codigo_exp_emp,
                        principalTable: "ExpedienteEmpleado",
                        principalColumn: "codigo_exp_emp",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpedienteDocumentoEmpleado_TipoDocumentoEmpleado_cod_tipo_doc_emp",
                        column: x => x.cod_tipo_doc_emp,
                        principalTable: "TipoDocumentoEmpleado",
                        principalColumn: "cod_tipo_doc_emp",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDocumentoEmpleado_cod_tipo_doc_emp",
                table: "ExpedienteDocumentoEmpleado",
                column: "cod_tipo_doc_emp");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDocumentoEmpleado_codigo_exp_emp",
                table: "ExpedienteDocumentoEmpleado",
                column: "codigo_exp_emp");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteEmpleado_dni_empleado",
                table: "ExpedienteEmpleado",
                column: "dni_empleado",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpedienteDocumentoEmpleado");

            migrationBuilder.DropTable(
                name: "ExpedienteEmpleado");

            migrationBuilder.DropTable(
                name: "TipoDocumentoEmpleado");
        }
    }
}
