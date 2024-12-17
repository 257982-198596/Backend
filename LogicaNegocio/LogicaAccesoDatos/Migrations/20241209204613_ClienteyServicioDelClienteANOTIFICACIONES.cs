using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class ClienteyServicioDelClienteANOTIFICACIONES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Clientes_ClienteId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_ClienteId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Notificaciones");

            migrationBuilder.AddColumn<int>(
                name: "ClienteNotificadoId",
                table: "Notificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicioNotificadoId",
                table: "Notificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ClienteNotificadoId",
                table: "Notificaciones",
                column: "ClienteNotificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ServicioNotificadoId",
                table: "Notificaciones",
                column: "ServicioNotificadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Clientes_ClienteNotificadoId",
                table: "Notificaciones",
                column: "ClienteNotificadoId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_ServiciosDelCliente_ServicioNotificadoId",
                table: "Notificaciones",
                column: "ServicioNotificadoId",
                principalTable: "ServiciosDelCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Clientes_ClienteNotificadoId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_ServiciosDelCliente_ServicioNotificadoId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_ClienteNotificadoId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_ServicioNotificadoId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "ClienteNotificadoId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "ServicioNotificadoId",
                table: "Notificaciones");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Notificaciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ClienteId",
                table: "Notificaciones",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Clientes_ClienteId",
                table: "Notificaciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
