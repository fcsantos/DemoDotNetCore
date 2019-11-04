using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace testeEFCore.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    TipoFornecedor = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FornecedorId = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FornecedorId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "DateTime", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime", nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_FornecedorId",
                table: "Endereco",
                column: "FornecedorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_FornecedorId",
                table: "Produto",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
