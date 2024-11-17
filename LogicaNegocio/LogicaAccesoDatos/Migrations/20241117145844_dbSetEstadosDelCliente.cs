using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class dbSetEstadosDelCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_EstadoCliente_EstadoId",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoCliente",
                table: "EstadoCliente");

            migrationBuilder.RenameTable(
                name: "EstadoCliente",
                newName: "EstadosDelCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosDelCliente",
                table: "EstadosDelCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_EstadosDelCliente_EstadoId",
                table: "Clientes",
                column: "EstadoId",
                principalTable: "EstadosDelCliente",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_EstadosDelCliente_EstadoId",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosDelCliente",
                table: "EstadosDelCliente");

            migrationBuilder.RenameTable(
                name: "EstadosDelCliente",
                newName: "EstadoCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoCliente",
                table: "EstadoCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_EstadoCliente_EstadoId",
                table: "Clientes",
                column: "EstadoId",
                principalTable: "EstadoCliente",
                principalColumn: "Id");
        }
    }
}
