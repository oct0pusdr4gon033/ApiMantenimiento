using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesCorregidas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Flota_id_modelo",
                table: "Flota",
                column: "id_modelo");

            migrationBuilder.AddForeignKey(
                name: "FK_Flota_ModeloEquipo_id_modelo",
                table: "Flota",
                column: "id_modelo",
                principalTable: "ModeloEquipo",
                principalColumn: "id_modelo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flota_ModeloEquipo_id_modelo",
                table: "Flota");

            migrationBuilder.DropIndex(
                name: "IX_Flota_id_modelo",
                table: "Flota");
        }
    }
}
