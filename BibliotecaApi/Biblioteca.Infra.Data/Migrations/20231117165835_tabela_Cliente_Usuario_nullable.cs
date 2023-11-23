using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class tabela_Cliente_Usuario_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_AspNetUsers_UsuarioId",
                table: "Cliente");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Cliente",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_AspNetUsers_UsuarioId",
                table: "Cliente",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_AspNetUsers_UsuarioId",
                table: "Cliente");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Cliente",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_AspNetUsers_UsuarioId",
                table: "Cliente",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
