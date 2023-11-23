using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Biblioteca.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_Tabela_LivroGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Livro");

            migrationBuilder.AddColumn<long>(
                name: "LivroGeneroId",
                table: "Livro",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "LivroGenero",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false),
                    Excluido = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExclusaoData = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroGenero", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_LivroGeneroId",
                table: "Livro",
                column: "LivroGeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_LivroGenero_LivroGeneroId",
                table: "Livro",
                column: "LivroGeneroId",
                principalTable: "LivroGenero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_LivroGenero_LivroGeneroId",
                table: "Livro");

            migrationBuilder.DropTable(
                name: "LivroGenero");

            migrationBuilder.DropIndex(
                name: "IX_Livro_LivroGeneroId",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "LivroGeneroId",
                table: "Livro");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Livro",
                type: "longtext",
                nullable: false);
        }
    }
}
