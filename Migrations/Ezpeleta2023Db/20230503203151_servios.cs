using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezpeleta2023.Migrations.Ezpeleta2023Db
{
    public partial class servios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ServicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    SubCategoriaID = table.Column<int>(type: "int", nullable: false),
                    Desabilitado = table.Column<bool>(type: "bit", nullable: false),
                    SubCategoriasSubCategoriaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.ServicioID);
                    table.ForeignKey(
                        name: "FK_Servicios_SubCategorias_SubCategoriasSubCategoriaID",
                        column: x => x.SubCategoriasSubCategoriaID,
                        principalTable: "SubCategorias",
                        principalColumn: "SubCategoriaID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_SubCategoriasSubCategoriaID",
                table: "Servicios",
                column: "SubCategoriasSubCategoriaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicios");
        }
    }
}
