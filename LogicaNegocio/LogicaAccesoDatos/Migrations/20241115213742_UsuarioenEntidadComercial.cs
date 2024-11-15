using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class UsuarioenEntidadComercial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioLoginId",
                table: "Suscriptores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioLoginId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suscriptores_UsuarioLoginId",
                table: "Suscriptores",
                column: "UsuarioLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioLoginId",
                table: "Clientes",
                column: "UsuarioLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioLoginId",
                table: "Clientes",
                column: "UsuarioLoginId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suscriptores_Usuarios_UsuarioLoginId",
                table: "Suscriptores",
                column: "UsuarioLoginId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioLoginId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Suscriptores_Usuarios_UsuarioLoginId",
                table: "Suscriptores");

            migrationBuilder.DropIndex(
                name: "IX_Suscriptores_UsuarioLoginId",
                table: "Suscriptores");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioLoginId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioLoginId",
                table: "Suscriptores");

            migrationBuilder.DropColumn(
                name: "UsuarioLoginId",
                table: "Clientes");
        }
    }
}
