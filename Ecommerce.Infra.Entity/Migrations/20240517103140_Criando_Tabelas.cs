using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Criando_Tabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Nome = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(type: "Bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preco = table.Column<decimal>(type: "Decimal(15,2)", nullable: false),
                    Descricao = table.Column<string>(type: "Varchar(100)", nullable: false),
                    FabricanteId = table.Column<int>(type: "INT", nullable: false),
                    UrlImagem = table.Column<string>(type: "Varchar(100)", nullable: false),
                    CategoriaId = table.Column<int>(type: "INT", nullable: false),
                    Nome = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(type: "Bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Produto_tbl_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "tbl_Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Produto_tbl_Fabricante_FabricanteId",
                        column: x => x.FabricanteId,
                        principalTable: "tbl_Fabricante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Produto_CategoriaId",
                table: "tbl_Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Produto_FabricanteId",
                table: "tbl_Produto",
                column: "FabricanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Produto");

            migrationBuilder.DropTable(
                name: "tbl_Categoria");
        }
    }
}
