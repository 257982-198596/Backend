using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class SeAgregaCascadaEntreClienteYUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioLoginId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioLoginId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioLoginId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioLoginId",
                table: "Clientes",
                column: "UsuarioLoginId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioLoginId",
                table: "Clientes",
                column: "UsuarioLoginId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioLoginId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioLoginId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioLoginId",
                table: "Clientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
        }
    }
}
