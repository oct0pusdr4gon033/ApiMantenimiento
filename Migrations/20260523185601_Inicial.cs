using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaOperacion",
                columns: table => new
                {
                    cod_area_ope = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    nomb_area = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaOperacion", x => x.cod_area_ope);
                });

            migrationBuilder.CreateTable(
                name: "Flota",
                columns: table => new
                {
                    id_flota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_flota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_modelo = table.Column<int>(type: "int", nullable: false),
                    nombre_flota = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tipo_control = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flota", x => x.id_flota);
                });

            migrationBuilder.CreateTable(
                name: "MarcaEquipo",
                columns: table => new
                {
                    id_marca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_marca = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaEquipo", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "TipoEquipos",
                columns: table => new
                {
                    id_tipo_eqp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_equipo = table.Column<string>(type: "char(10)", nullable: false),
                    nombre_tipo = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEquipos", x => x.id_tipo_eqp);
                });

            migrationBuilder.CreateTable(
                name: "Equipo",
                columns: table => new
                {
                    id_equipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_flota = table.Column<int>(type: "int", nullable: false),
                    cod_eqp = table.Column<string>(type: "char(18)", nullable: false),
                    placa_eqp = table.Column<string>(type: "varchar(10)", nullable: false),
                    num_serie = table.Column<string>(type: "varchar(100)", nullable: false),
                    horometro_inicial = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    horometro_actual = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    estado_operativo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cod_are_ope = table.Column<string>(type: "varchar(10)", nullable: false),
                    AreaOperacioncod_area_ope = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipo", x => x.id_equipo);
                    table.ForeignKey(
                        name: "FK_Equipo_AreaOperacion_AreaOperacioncod_area_ope",
                        column: x => x.AreaOperacioncod_area_ope,
                        principalTable: "AreaOperacion",
                        principalColumn: "cod_area_ope",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipo_Flota_id_flota",
                        column: x => x.id_flota,
                        principalTable: "Flota",
                        principalColumn: "id_flota",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloEquipo",
                columns: table => new
                {
                    id_modelo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_marca = table.Column<int>(type: "int", nullable: false),
                    id_tipo_eqp = table.Column<int>(type: "int", nullable: false),
                    nombre_modelo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEquipo", x => x.id_modelo);
                    table.ForeignKey(
                        name: "FK_ModeloEquipo_MarcaEquipo_id_marca",
                        column: x => x.id_marca,
                        principalTable: "MarcaEquipo",
                        principalColumn: "id_marca",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEquipo_TipoEquipos_id_tipo_eqp",
                        column: x => x.id_tipo_eqp,
                        principalTable: "TipoEquipos",
                        principalColumn: "id_tipo_eqp",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_AreaOperacioncod_area_ope",
                table: "Equipo",
                column: "AreaOperacioncod_area_ope");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_id_flota",
                table: "Equipo",
                column: "id_flota");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEquipo_id_marca",
                table: "ModeloEquipo",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEquipo_id_tipo_eqp",
                table: "ModeloEquipo",
                column: "id_tipo_eqp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipo");

            migrationBuilder.DropTable(
                name: "ModeloEquipo");

            migrationBuilder.DropTable(
                name: "AreaOperacion");

            migrationBuilder.DropTable(
                name: "Flota");

            migrationBuilder.DropTable(
                name: "MarcaEquipo");

            migrationBuilder.DropTable(
                name: "TipoEquipos");
        }
    }
}
