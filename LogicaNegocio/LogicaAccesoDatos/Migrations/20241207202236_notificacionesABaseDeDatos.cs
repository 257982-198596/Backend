using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class notificacionesABaseDeDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificacion_Clientes_ClienteId",
                table: "Notificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificacion_EstadoNotificacion_EstadoDeNotificacionId",
                table: "Notificacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notificacion",
                table: "Notificacion");

            migrationBuilder.RenameTable(
                name: "Notificacion",
                newName: "Notificaciones");

            migrationBuilder.RenameIndex(
                name: "IX_Notificacion_EstadoDeNotificacionId",
                table: "Notificaciones",
                newName: "IX_Notificaciones_EstadoDeNotificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Notificacion_ClienteId",
                table: "Notificaciones",
                newName: "IX_Notificaciones_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Clientes_ClienteId",
                table: "Notificaciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_EstadoNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones",
                column: "EstadoDeNotificacionId",
                principalTable: "EstadoNotificacion",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Clientes_ClienteId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_EstadoNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones");

            migrationBuilder.RenameTable(
                name: "Notificaciones",
                newName: "Notificacion");

            migrationBuilder.RenameIndex(
                name: "IX_Notificaciones_EstadoDeNotificacionId",
                table: "Notificacion",
                newName: "IX_Notificacion_EstadoDeNotificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Notificaciones_ClienteId",
                table: "Notificacion",
                newName: "IX_Notificacion_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notificacion",
                table: "Notificacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacion_Clientes_ClienteId",
                table: "Notificacion",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacion_EstadoNotificacion_EstadoDeNotificacionId",
                table: "Notificacion",
                column: "EstadoDeNotificacionId",
                principalTable: "EstadoNotificacion",
                principalColumn: "Id");
        }
    }
}
