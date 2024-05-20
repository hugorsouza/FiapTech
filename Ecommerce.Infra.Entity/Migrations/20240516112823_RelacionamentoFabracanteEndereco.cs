using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.Entity.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoFabracanteEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Fabricante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "Varchar(14)", nullable: false),
                    EnderecoId = table.Column<int>(type: "INT", nullable: false),
                    Nome = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(type: "Bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Fabricante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Fabricante_tbl_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "tbl_Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Fabricante_EnderecoId",
                table: "tbl_Fabricante",
                column: "EnderecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Fabricante");
        }
    }
}
