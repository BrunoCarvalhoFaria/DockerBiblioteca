using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_LivroGenero_inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO LivroGenero (Descricao, Excluido) values ('SUSPENSE' ,0)");
            migrationBuilder.Sql($@"INSERT INTO LivroGenero (Descricao, Excluido) values ('ROMANCE' ,0)");
            migrationBuilder.Sql($@"INSERT INTO LivroGenero (Descricao, Excluido) values ('TECNOLOGIA' ,0)");
            migrationBuilder.Sql($@"INSERT INTO LivroGenero (Descricao, Excluido) values ('INFANTIL' ,0)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
