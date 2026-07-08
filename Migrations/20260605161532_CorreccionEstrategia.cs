using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionEstrategia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Man_PlanMantenimiento_EstrategiaDetalle_id_detalle_estrg",
                table: "Man_PlanMantenimiento");

            migrationBuilder.DropTable(
                name: "Man_PlanMantenimientoMaterial");

            migrationBuilder.DropIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_plan_mant_id_actividad",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.RenameColumn(
                name: "id_detalle_estrg",
                table: "Man_PlanMantenimiento",
                newName: "id_estrategia");

            migrationBuilder.RenameIndex(
                name: "IX_Man_PlanMantenimiento_id_detalle_estrg",
                table: "Man_PlanMantenimiento",
                newName: "IX_Man_PlanMantenimiento_id_estrategia");

            migrationBuilder.AddColumn<decimal>(
                name: "cantidad",
                table: "Man_PlanMantenimientoActividad",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_detalle_estrg",
                table: "Man_PlanMantenimientoActividad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_material",
                table: "Man_PlanMantenimientoActividad",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_detalle_estrg",
                table: "Man_PlanMantenimientoActividad",
                column: "id_detalle_estrg");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_material",
                table: "Man_PlanMantenimientoActividad",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_plan_mant",
                table: "Man_PlanMantenimientoActividad",
                column: "id_plan_mant");

            migrationBuilder.AddForeignKey(
                name: "FK_Man_PlanMantenimiento_Estrategia_id_estrategia",
                table: "Man_PlanMantenimiento",
                column: "id_estrategia",
                principalTable: "Estrategia",
                principalColumn: "id_estrategia",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Man_PlanMantenimientoActividad_Alm_Material_id_material",
                table: "Man_PlanMantenimientoActividad",
                column: "id_material",
                principalTable: "Alm_Material",
                principalColumn: "id_material",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Man_PlanMantenimientoActividad_EstrategiaDetalle_id_detalle_estrg",
                table: "Man_PlanMantenimientoActividad",
                column: "id_detalle_estrg",
                principalTable: "EstrategiaDetalle",
                principalColumn: "id_detalle_estrg",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Man_PlanMantenimiento_Estrategia_id_estrategia",
                table: "Man_PlanMantenimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_Man_PlanMantenimientoActividad_Alm_Material_id_material",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropForeignKey(
                name: "FK_Man_PlanMantenimientoActividad_EstrategiaDetalle_id_detalle_estrg",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_detalle_estrg",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_material",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_plan_mant",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropColumn(
                name: "cantidad",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropColumn(
                name: "id_detalle_estrg",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropColumn(
                name: "id_material",
                table: "Man_PlanMantenimientoActividad");

            migrationBuilder.RenameColumn(
                name: "id_estrategia",
                table: "Man_PlanMantenimiento",
                newName: "id_detalle_estrg");

            migrationBuilder.RenameIndex(
                name: "IX_Man_PlanMantenimiento_id_estrategia",
                table: "Man_PlanMantenimiento",
                newName: "IX_Man_PlanMantenimiento_id_detalle_estrg");

            migrationBuilder.CreateTable(
                name: "Man_PlanMantenimientoMaterial",
                columns: table => new
                {
                    id_plan_material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    id_plan_mant = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_PlanMantenimientoMaterial", x => x.id_plan_material);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimientoMaterial_Alm_Material_id_material",
                        column: x => x.id_material,
                        principalTable: "Alm_Material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimientoMaterial_Man_PlanMantenimiento_id_plan_mant",
                        column: x => x.id_plan_mant,
                        principalTable: "Man_PlanMantenimiento",
                        principalColumn: "id_plan_mant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_plan_mant_id_actividad",
                table: "Man_PlanMantenimientoActividad",
                columns: new[] { "id_plan_mant", "id_actividad" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoMaterial_id_material",
                table: "Man_PlanMantenimientoMaterial",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoMaterial_id_plan_mant_id_material",
                table: "Man_PlanMantenimientoMaterial",
                columns: new[] { "id_plan_mant", "id_material" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Man_PlanMantenimiento_EstrategiaDetalle_id_detalle_estrg",
                table: "Man_PlanMantenimiento",
                column: "id_detalle_estrg",
                principalTable: "EstrategiaDetalle",
                principalColumn: "id_detalle_estrg",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
