using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class ACTIVIDADESSISTEMA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sistema_equipo",
                columns: table => new
                {
                    id_sistema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_sist = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nombre_sist = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sistema_equipo", x => x.id_sistema);
                });

            migrationBuilder.CreateTable(
                name: "actividades_sistema",
                columns: table => new
                {
                    id_actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_act = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_sistema = table.Column<int>(type: "int", nullable: false),
                    nombre_actividad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    duracion = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actividades_sistema", x => x.id_actividad);
                    table.ForeignKey(
                        name: "FK_actividades_sistema_sistema_equipo_id_sistema",
                        column: x => x.id_sistema,
                        principalTable: "sistema_equipo",
                        principalColumn: "id_sistema",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actividades_sistema_id_sistema",
                table: "actividades_sistema",
                column: "id_sistema");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actividades_sistema");

            migrationBuilder.DropTable(
                name: "sistema_equipo");
        }
    }
}
