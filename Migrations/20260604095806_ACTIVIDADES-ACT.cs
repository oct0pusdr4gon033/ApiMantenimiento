using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class ACTIVIDADESACT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "medida_duracion",
                table: "actividades_sistema",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "medida_duracion",
                table: "actividades_sistema");
        }
    }
}
