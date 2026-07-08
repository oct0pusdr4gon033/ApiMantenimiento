using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesCorregidas_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipo_AreaOperacion_AreaOperacioncod_area_ope",
                table: "Equipo");

            migrationBuilder.DropIndex(
                name: "IX_Equipo_AreaOperacioncod_area_ope",
                table: "Equipo");

            migrationBuilder.DropColumn(
                name: "AreaOperacioncod_area_ope",
                table: "Equipo");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_cod_are_ope",
                table: "Equipo",
                column: "cod_are_ope");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipo_AreaOperacion_cod_are_ope",
                table: "Equipo",
                column: "cod_are_ope",
                principalTable: "AreaOperacion",
                principalColumn: "cod_area_ope",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipo_AreaOperacion_cod_are_ope",
                table: "Equipo");

            migrationBuilder.DropIndex(
                name: "IX_Equipo_cod_are_ope",
                table: "Equipo");

            migrationBuilder.AddColumn<string>(
                name: "AreaOperacioncod_area_ope",
                table: "Equipo",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_AreaOperacioncod_area_ope",
                table: "Equipo",
                column: "AreaOperacioncod_area_ope");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipo_AreaOperacion_AreaOperacioncod_area_ope",
                table: "Equipo",
                column: "AreaOperacioncod_area_ope",
                principalTable: "AreaOperacion",
                principalColumn: "cod_area_ope",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
