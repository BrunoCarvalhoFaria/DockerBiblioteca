using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Retirada_CascadeMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "CascadeMode",
            //    table: "LivroGenero");

            //migrationBuilder.DropColumn(
            //    name: "CascadeMode",
            //    table: "Livro");

            //migrationBuilder.DropColumn(
            //    name: "CascadeMode",
            //    table: "Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "CascadeMode",
            //    table: "LivroGenero",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "CascadeMode",
            //    table: "Livro",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "CascadeMode",
            //    table: "Cliente",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
