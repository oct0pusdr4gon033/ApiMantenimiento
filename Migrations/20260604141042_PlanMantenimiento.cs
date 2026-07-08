using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class PlanMantenimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alm_CategoriaMaterial",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_cat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombre_categoria = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_CategoriaMaterial", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Alm_UnidadMedida",
                columns: table => new
                {
                    id_unidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_unidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_UnidadMedida", x => x.id_unidad);
                });

            migrationBuilder.CreateTable(
                name: "Man_PlanMantenimiento",
                columns: table => new
                {
                    id_plan_mant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_detalle_estrg = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_PlanMantenimiento", x => x.id_plan_mant);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimiento_EstrategiaDetalle_id_detalle_estrg",
                        column: x => x.id_detalle_estrg,
                        principalTable: "EstrategiaDetalle",
                        principalColumn: "id_detalle_estrg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alm_Material",
                columns: table => new
                {
                    id_material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_unidad = table.Column<int>(type: "int", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    cod_materia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alm_Material", x => x.id_material);
                    table.ForeignKey(
                        name: "FK_Alm_Material_Alm_CategoriaMaterial_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Alm_CategoriaMaterial",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alm_Material_Alm_UnidadMedida_id_unidad",
                        column: x => x.id_unidad,
                        principalTable: "Alm_UnidadMedida",
                        principalColumn: "id_unidad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Man_PlanMantenimientoActividad",
                columns: table => new
                {
                    id_plan_actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_plan_mant = table.Column<int>(type: "int", nullable: false),
                    id_actividad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_PlanMantenimientoActividad", x => x.id_plan_actividad);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimientoActividad_Man_PlanMantenimiento_id_plan_mant",
                        column: x => x.id_plan_mant,
                        principalTable: "Man_PlanMantenimiento",
                        principalColumn: "id_plan_mant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimientoActividad_actividades_sistema_id_actividad",
                        column: x => x.id_actividad,
                        principalTable: "actividades_sistema",
                        principalColumn: "id_actividad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Man_PlanMantenimientoPersonal",
                columns: table => new
                {
                    id_plan_personal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_plan_mant = table.Column<int>(type: "int", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Man_PlanMantenimientoPersonal", x => x.id_plan_personal);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimientoPersonal_Man_PlanMantenimiento_id_plan_mant",
                        column: x => x.id_plan_mant,
                        principalTable: "Man_PlanMantenimiento",
                        principalColumn: "id_plan_mant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Man_PlanMantenimientoPersonal_Rol_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Man_PlanMantenimientoMaterial",
                columns: table => new
                {
                    id_plan_material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_plan_mant = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_Alm_CategoriaMaterial_cod_cat",
                table: "Alm_CategoriaMaterial",
                column: "cod_cat",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alm_Material_cod_materia",
                table: "Alm_Material",
                column: "cod_materia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alm_Material_id_categoria",
                table: "Alm_Material",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Alm_Material_id_unidad",
                table: "Alm_Material",
                column: "id_unidad");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimiento_id_detalle_estrg",
                table: "Man_PlanMantenimiento",
                column: "id_detalle_estrg");

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoActividad_id_actividad",
                table: "Man_PlanMantenimientoActividad",
                column: "id_actividad");

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

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoPersonal_id_plan_mant_id_rol",
                table: "Man_PlanMantenimientoPersonal",
                columns: new[] { "id_plan_mant", "id_rol" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Man_PlanMantenimientoPersonal_id_rol",
                table: "Man_PlanMantenimientoPersonal",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Man_PlanMantenimientoActividad");

            migrationBuilder.DropTable(
                name: "Man_PlanMantenimientoMaterial");

            migrationBuilder.DropTable(
                name: "Man_PlanMantenimientoPersonal");

            migrationBuilder.DropTable(
                name: "Alm_Material");

            migrationBuilder.DropTable(
                name: "Man_PlanMantenimiento");

            migrationBuilder.DropTable(
                name: "Alm_CategoriaMaterial");

            migrationBuilder.DropTable(
                name: "Alm_UnidadMedida");
        }
    }
}
