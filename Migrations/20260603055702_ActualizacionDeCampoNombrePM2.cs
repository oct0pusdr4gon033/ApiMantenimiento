using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionDeCampoNombrePM2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo_pm",
                table: "Estrategia");

            migrationBuilder.AddColumn<string>(
                name: "tipo_pm",
                table: "EstrategiaDetalle",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo_pm",
                table: "EstrategiaDetalle");

            migrationBuilder.AddColumn<string>(
                name: "tipo_pm",
                table: "Estrategia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
