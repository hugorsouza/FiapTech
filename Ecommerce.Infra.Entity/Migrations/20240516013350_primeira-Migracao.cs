using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.Entity.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Numero = table.Column<string>(type: "Varchar(100)", nullable: false),
                    CEP = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Bairro = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "Varchar(100)", nullable: false),
                    EntidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Endereco", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Endereco");
        }
    }
}
