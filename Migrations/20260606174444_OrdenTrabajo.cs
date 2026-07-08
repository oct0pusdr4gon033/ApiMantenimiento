using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class OrdenTrabajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Man_ConfiguracionFlota",
                columns: table => new
                {
                    id_configuracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_flota = table.Column<int>(type: "int", nullable: true),
                    horas_diarias_estimadas = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 12m),
                    fecha_actualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    actualizado_por = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_ConfiguracionFlota", x => x.id_configuracion);
                    table.ForeignKey(
                        name: "FK_Man_ConfiguracionFlota_Flota_id_flota",
                        column: x => x.id_flota,
                        principalTable: "Flota",
                        principalColumn: "id_flota",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Man_OrdenTrabajo",
                columns: table => new
                {
                    id_ot = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_ot = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_equipo = table.Column<int>(type: "int", nullable: false),
                    id_plan_mant = table.Column<int>(type: "int", nullable: false),
                    tipo_ot = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    forma_generacion = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: "PENDIENTE"),
                    horometro_al_momento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    horometro_corte = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_atencion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    creado_por = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_OrdenTrabajo", x => x.id_ot);
                    table.ForeignKey(
                        name: "FK_Man_OrdenTrabajo_Equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "Equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_OrdenTrabajo_Man_PlanMantenimiento_id_plan_mant",
                        column: x => x.id_plan_mant,
                        principalTable: "Man_PlanMantenimiento",
                        principalColumn: "id_plan_mant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Man_CalendarioMantenimiento",
                columns: table => new
                {
                    id_calendario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_equipo = table.Column<int>(type: "int", nullable: false),
                    id_detalle_estrg = table.Column<int>(type: "int", nullable: false),
                    horometro_base = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_base = table.Column<DateTime>(type: "datetime2", nullable: false),
                    horas_diarias_usadas = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    fecha_estimada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_ot = table.Column<int>(type: "int", nullable: true),
                    ejecutado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    fecha_real_ejecucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_CalendarioMantenimiento", x => x.id_calendario);
                    table.ForeignKey(
                        name: "FK_Man_CalendarioMantenimiento_Equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "Equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_CalendarioMantenimiento_EstrategiaDetalle_id_detalle_estrg",
                        column: x => x.id_detalle_estrg,
                        principalTable: "EstrategiaDetalle",
                        principalColumn: "id_detalle_estrg",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_CalendarioMantenimiento_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Man_OTActividad",
                columns: table => new
                {
                    id_ot_actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ot = table.Column<int>(type: "int", nullable: false),
                    id_actividad_ref = table.Column<int>(type: "int", nullable: true),
                    nombre_actividad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    cod_sistema = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tipo_pm = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    estado_ejecucion = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: "PENDIENTE"),
                    observacion_tecnica = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_OTActividad", x => x.id_ot_actividad);
                    table.ForeignKey(
                        name: "FK_Man_OTActividad_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Man_OTMaterial",
                columns: table => new
                {
                    id_ot_material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ot = table.Column<int>(type: "int", nullable: false),
                    id_material_ref = table.Column<int>(type: "int", nullable: true),
                    cod_materia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    descripcion_material = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    cantidad_requerida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cantidad_utilizada = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_OTMaterial", x => x.id_ot_material);
                    table.ForeignKey(
                        name: "FK_Man_OTMaterial_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Man_OTPersonal",
                columns: table => new
                {
                    id_ot_personal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ot = table.Column<int>(type: "int", nullable: false),
                    dni_empleado = table.Column<string>(type: "char(8)", maxLength: 15, nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_OTPersonal", x => x.id_ot_personal);
                    table.ForeignKey(
                        name: "FK_Man_OTPersonal_Empleado_dni_empleado",
                        column: x => x.dni_empleado,
                        principalTable: "Empleado",
                        principalColumn: "dni_empleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_OTPersonal_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Man_OTPlanDetalle",
                columns: table => new
                {
                    id_ot = table.Column<int>(type: "int", nullable: false),
                    id_detalle_estrg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_OTPlanDetalle", x => new { x.id_ot, x.id_detalle_estrg });
                    table.ForeignKey(
                        name: "FK_Man_OTPlanDetalle_EstrategiaDetalle_id_detalle_estrg",
                        column: x => x.id_detalle_estrg,
                        principalTable: "EstrategiaDetalle",
                        principalColumn: "id_detalle_estrg",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_OTPlanDetalle_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Man_PMUltimaIntervencion",
                columns: table => new
                {
                    id_equipo = table.Column<int>(type: "int", nullable: false),
                    id_detalle_estrg = table.Column<int>(type: "int", nullable: false),
                    horometro_corte = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    fecha_corte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_ot = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_PMUltimaIntervencion", x => new { x.id_equipo, x.id_detalle_estrg });
                    table.ForeignKey(
                        name: "FK_Man_PMUltimaIntervencion_Equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "Equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_PMUltimaIntervencion_EstrategiaDetalle_id_detalle_estrg",
                        column: x => x.id_detalle_estrg,
                        principalTable: "EstrategiaDetalle",
                        principalColumn: "id_detalle_estrg",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_PMUltimaIntervencion_Man_OrdenTrabajo_id_ot",
                        column: x => x.id_ot,
                        principalTable: "Man_OrdenTrabajo",
                        principalColumn: "id_ot",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Man_CalendarioMantenimiento_id_detalle_estrg",
                table: "Man_CalendarioMantenimiento",
                column: "id_detalle_estrg");

            migrationBuilder.CreateIndex(
                name: "IX_Man_CalendarioMantenimiento_id_equipo_id_detalle_estrg_ejecutado",
                table: "Man_CalendarioMantenimiento",
                columns: new[] { "id_equipo", "id_detalle_estrg", "ejecutado" });

            migrationBuilder.CreateIndex(
                name: "IX_Man_CalendarioMantenimiento_id_ot",
                table: "Man_CalendarioMantenimiento",
                column: "id_ot");

            migrationBuilder.CreateIndex(
                name: "IX_Man_ConfiguracionFlota_id_flota",
                table: "Man_ConfiguracionFlota",
                column: "id_flota");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OrdenTrabajo_cod_ot",
                table: "Man_OrdenTrabajo",
                column: "cod_ot",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Man_OrdenTrabajo_id_equipo",
                table: "Man_OrdenTrabajo",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OrdenTrabajo_id_plan_mant",
                table: "Man_OrdenTrabajo",
                column: "id_plan_mant");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OTActividad_id_ot",
                table: "Man_OTActividad",
                column: "id_ot");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OTMaterial_id_ot",
                table: "Man_OTMaterial",
                column: "id_ot");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OTPersonal_dni_empleado",
                table: "Man_OTPersonal",
                column: "dni_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OTPersonal_id_ot",
                table: "Man_OTPersonal",
                column: "id_ot");

            migrationBuilder.CreateIndex(
                name: "IX_Man_OTPlanDetalle_id_detalle_estrg",
                table: "Man_OTPlanDetalle",
                column: "id_detalle_estrg");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PMUltimaIntervencion_id_detalle_estrg",
                table: "Man_PMUltimaIntervencion",
                column: "id_detalle_estrg");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PMUltimaIntervencion_id_ot",
                table: "Man_PMUltimaIntervencion",
                column: "id_ot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Man_CalendarioMantenimiento");

            migrationBuilder.DropTable(
                name: "Man_ConfiguracionFlota");

            migrationBuilder.DropTable(
                name: "Man_OTActividad");

            migrationBuilder.DropTable(
                name: "Man_OTMaterial");

            migrationBuilder.DropTable(
                name: "Man_OTPersonal");

            migrationBuilder.DropTable(
                name: "Man_OTPlanDetalle");

            migrationBuilder.DropTable(
                name: "Man_PMUltimaIntervencion");

            migrationBuilder.DropTable(
                name: "Man_OrdenTrabajo");
        }
    }
}
