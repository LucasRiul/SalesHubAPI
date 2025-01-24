using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LOTES_PRODUTOS_ProdutoidProduto",
                table: "LOTES");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_FORNECEDORES_FornecedoridFornecedor",
                table: "PRODUTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_USUARIOS_UsuarioidUsuario",
                table: "PRODUTOS");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioidUsuario",
                table: "PRODUTOS",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FornecedoridFornecedor",
                table: "PRODUTOS",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ProdutoidProduto",
                table: "LOTES",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_LOTES_PRODUTOS_ProdutoidProduto",
                table: "LOTES",
                column: "ProdutoidProduto",
                principalTable: "PRODUTOS",
                principalColumn: "idProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOS_FORNECEDORES_FornecedoridFornecedor",
                table: "PRODUTOS",
                column: "FornecedoridFornecedor",
                principalTable: "FORNECEDORES",
                principalColumn: "idFornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOS_USUARIOS_UsuarioidUsuario",
                table: "PRODUTOS",
                column: "UsuarioidUsuario",
                principalTable: "USUARIOS",
                principalColumn: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LOTES_PRODUTOS_ProdutoidProduto",
                table: "LOTES");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_FORNECEDORES_FornecedoridFornecedor",
                table: "PRODUTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTOS_USUARIOS_UsuarioidUsuario",
                table: "PRODUTOS");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioidUsuario",
                table: "PRODUTOS",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FornecedoridFornecedor",
                table: "PRODUTOS",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProdutoidProduto",
                table: "LOTES",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LOTES_PRODUTOS_ProdutoidProduto",
                table: "LOTES",
                column: "ProdutoidProduto",
                principalTable: "PRODUTOS",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOS_FORNECEDORES_FornecedoridFornecedor",
                table: "PRODUTOS",
                column: "FornecedoridFornecedor",
                principalTable: "FORNECEDORES",
                principalColumn: "idFornecedor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTOS_USUARIOS_UsuarioidUsuario",
                table: "PRODUTOS",
                column: "UsuarioidUsuario",
                principalTable: "USUARIOS",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
