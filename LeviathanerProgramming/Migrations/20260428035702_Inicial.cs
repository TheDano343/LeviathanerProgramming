using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeviathanerProgramming.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lenguaje",
                columns: table => new
                {
                    IdLenguaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreLenguaje = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lenguaje", x => x.IdLenguaje);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NivelDificultad",
                columns: table => new
                {
                    IdNivelDificultad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreDificultad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelDificultad", x => x.IdNivelDificultad);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoadMap",
                columns: table => new
                {
                    IdRoadMap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lenguaje_Programacion_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadMap", x => x.IdRoadMap);
                    table.ForeignKey(
                        name: "FK_RoadMap_Lenguaje_Lenguaje_Programacion_Id",
                        column: x => x.Lenguaje_Programacion_Id,
                        principalTable: "Lenguaje",
                        principalColumn: "IdLenguaje",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nivel_Dificiltad_Id = table.Column<int>(type: "int", nullable: false),
                    Road_Map_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.IdPost);
                    table.ForeignKey(
                        name: "FK_Post_NivelDificultad_Nivel_Dificiltad_Id",
                        column: x => x.Nivel_Dificiltad_Id,
                        principalTable: "NivelDificultad",
                        principalColumn: "IdNivelDificultad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_RoadMap_Road_Map_Id",
                        column: x => x.Road_Map_Id,
                        principalTable: "RoadMap",
                        principalColumn: "IdRoadMap",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoRutaAprendizaje",
                columns: table => new
                {
                    IdRutaAprendizaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTipoAprenidzaje = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Road_Map_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRutaAprendizaje", x => x.IdRutaAprendizaje);
                    table.ForeignKey(
                        name: "FK_TipoRutaAprendizaje_RoadMap_Road_Map_Id",
                        column: x => x.Road_Map_Id,
                        principalTable: "RoadMap",
                        principalColumn: "IdRoadMap",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Nivel_Dificiltad_Id",
                table: "Post",
                column: "Nivel_Dificiltad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Road_Map_Id",
                table: "Post",
                column: "Road_Map_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoadMap_Lenguaje_Programacion_Id",
                table: "RoadMap",
                column: "Lenguaje_Programacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TipoRutaAprendizaje_Road_Map_Id",
                table: "TipoRutaAprendizaje",
                column: "Road_Map_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "TipoRutaAprendizaje");

            migrationBuilder.DropTable(
                name: "NivelDificultad");

            migrationBuilder.DropTable(
                name: "RoadMap");

            migrationBuilder.DropTable(
                name: "Lenguaje");
        }
    }
}
