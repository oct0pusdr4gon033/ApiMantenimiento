using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class Correccion3FN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Man_PlanMantenimientoActividad_Alm_Material_id_material",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Man_PlanMantenimientoActividad",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_material",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_plan_mant",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropColumn(
                name: "id_plan_actividad",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropColumn(
                name: "cantidad",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropColumn(
                name: "id_material",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Man_PlanMantenimientoActividad",
                table: "Man_PlanMantenimientoActividad",
                columns: new[] { "id_plan_mant", "id_actividad", "id_detalle_estrg" });

            migrationBuilder.CreateTable(
                name: "Man_PlanActividadMaterial",
                columns: table => new
                {
                    id_plan_mant = table.Column<int>(type: "int", nullable: false),
                    id_actividad = table.Column<int>(type: "int", nullable: false),
                    id_detalle_estrg = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_PlanActividadMaterial", x => new { x.id_plan_mant, x.id_actividad, x.id_detalle_estrg, x.id_material });
                    table.ForeignKey(
                        name: "FK_Man_PlanActividadMaterial_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_PlanActividadMaterial_Man_PlanMantenimientoActividad_id_plan_mant_id_actividad_id_detalle_estrg",
                        columns: x => new { x.id_plan_mant, x.id_actividad, x.id_detalle_estrg },
                        principalTable: "Man_PlanMantenimientoActividad",
                        principalColumns: new[] { "id_plan_mant", "id_actividad", "id_detalle_estrg" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanActividadMaterial_id_material",
                table: "Man_PlanActividadMaterial",
                column: "id_material");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Man_PlanActividadMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Man_PlanMantenimientoActividad",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.AddColumn<int>(
                name: "id_plan_actividad",
                table: "Man_PlanMantenimientoActividad",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "cantidad",
                table: "Man_PlanMantenimientoActividad",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_material",
                table: "Man_PlanMantenimientoActividad",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Man_PlanMantenimientoActividad",
                table: "Man_PlanMantenimientoActividad",
                column: "id_plan_actividad");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_material",
                table: "Man_PlanMantenimientoActividad",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_plan_mant",
                table: "Man_PlanMantenimientoActividad",
                column: "id_plan_mant");

            migrationBuilder.AddForeignKey(
                name: "FK_Man_PlanMantenimientoActividad_Alm_Material_id_material",
                table: "Man_PlanMantenimientoActividad",
                column: "id_material",
                principalTable: "Alm_Material",
                principalColumn: "id_material",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
