using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESAS",
                columns: table => new
                {
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegimeTributario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESAS", x => x.idEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "FORNECEDORES",
                columns: table => new
                {
                    idFornecedor = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CPNJ = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Telefone1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDORES", x => x.idFornecedor);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    idUsuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_USUARIOS_EMPRESAS_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "EMPRESAS",
                        principalColumn: "idEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTOS",
                columns: table => new
                {
                    idProduto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstoqueMinimo = table.Column<int>(type: "int", nullable: false),
                    EstoqueInicial = table.Column<int>(type: "int", nullable: false),
                    EstoqueAtual = table.Column<int>(type: "int", nullable: false),
                    PrecoCusto = table.Column<float>(type: "real", nullable: false),
                    PrecoVenda = table.Column<float>(type: "real", nullable: false),
                    LUCRO = table.Column<float>(type: "real", nullable: false),
                    ICMS = table.Column<float>(type: "real", nullable: false),
                    ISS = table.Column<float>(type: "real", nullable: false),
                    COFINS = table.Column<float>(type: "real", nullable: false),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    idFornecedor = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioidUsuario = table.Column<long>(type: "bigint", nullable: false),
                    FornecedoridFornecedor = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOS", x => x.idProduto);
                    table.ForeignKey(
                        name: "FK_PRODUTOS_FORNECEDORES_FornecedoridFornecedor",
                        column: x => x.FornecedoridFornecedor,
                        principalTable: "FORNECEDORES",
                        principalColumn: "idFornecedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUTOS_USUARIOS_UsuarioidUsuario",
                        column: x => x.UsuarioidUsuario,
                        principalTable: "USUARIOS",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VENDAS",
                columns: table => new
                {
                    idVenda = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDAS", x => x.idVenda);
                    table.ForeignKey(
                        name: "FK_VENDAS_USUARIOS_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "USUARIOS",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LOTES",
                columns: table => new
                {
                    idLote = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idFornecedor = table.Column<long>(type: "bigint", nullable: false),
                    idProduto = table.Column<long>(type: "bigint", nullable: false),
                    ProdutoidProduto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOTES", x => x.idLote);
                    table.ForeignKey(
                        name: "FK_LOTES_FORNECEDORES_idFornecedor",
                        column: x => x.idFornecedor,
                        principalTable: "FORNECEDORES",
                        principalColumn: "idFornecedor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LOTES_PRODUTOS_ProdutoidProduto",
                        column: x => x.ProdutoidProduto,
                        principalTable: "PRODUTOS",
                        principalColumn: "idProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_VENDAS",
                columns: table => new
                {
                    idProduto = table.Column<long>(type: "bigint", nullable: false),
                    idVenda = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_VENDAS", x => new { x.idVenda, x.idProduto });
                    table.ForeignKey(
                        name: "FK_PRODUTO_VENDAS_PRODUTOS_idProduto",
                        column: x => x.idProduto,
                        principalTable: "PRODUTOS",
                        principalColumn: "idProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUTO_VENDAS_VENDAS_idVenda",
                        column: x => x.idVenda,
                        principalTable: "VENDAS",
                        principalColumn: "idVenda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LOTES_idFornecedor",
                table: "LOTES",
                column: "idFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_LOTES_ProdutoidProduto",
                table: "LOTES",
                column: "ProdutoidProduto");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_VENDAS_idProduto",
                table: "PRODUTO_VENDAS",
                column: "idProduto");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOS_FornecedoridFornecedor",
                table: "PRODUTOS",
                column: "FornecedoridFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOS_UsuarioidUsuario",
                table: "PRODUTOS",
                column: "UsuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_idEmpresa",
                table: "USUARIOS",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_VENDAS_idUsuario",
                table: "VENDAS",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOTES");

            migrationBuilder.DropTable(
                name: "PRODUTO_VENDAS");

            migrationBuilder.DropTable(
                name: "PRODUTOS");

            migrationBuilder.DropTable(
                name: "VENDAS");

            migrationBuilder.DropTable(
                name: "FORNECEDORES");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "EMPRESAS");
        }
    }
}
