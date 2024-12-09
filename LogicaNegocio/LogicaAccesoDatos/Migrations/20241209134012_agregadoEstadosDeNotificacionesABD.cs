using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class agregadoEstadosDeNotificacionesABD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_EstadoNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoNotificacion",
                table: "EstadoNotificacion");

            migrationBuilder.RenameTable(
                name: "EstadoNotificacion",
                newName: "EstadosDeNotificacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosDeNotificacion",
                table: "EstadosDeNotificacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_EstadosDeNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones",
                column: "EstadoDeNotificacionId",
                principalTable: "EstadosDeNotificacion",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_EstadosDeNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosDeNotificacion",
                table: "EstadosDeNotificacion");

            migrationBuilder.RenameTable(
                name: "EstadosDeNotificacion",
                newName: "EstadoNotificacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoNotificacion",
                table: "EstadoNotificacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_EstadoNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones",
                column: "EstadoDeNotificacionId",
                principalTable: "EstadoNotificacion",
                principalColumn: "Id");
        }
    }
}
