using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class relacionEntreNotificacionYSusEstadosPosibles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_EstadosDeNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoDeNotificacionId",
                table: "Notificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_EstadosDeNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones",
                column: "EstadoDeNotificacionId",
                principalTable: "EstadosDeNotificacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_EstadosDeNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoDeNotificacionId",
                table: "Notificaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_EstadosDeNotificacion_EstadoDeNotificacionId",
                table: "Notificaciones",
                column: "EstadoDeNotificacionId",
                principalTable: "EstadosDeNotificacion",
                principalColumn: "Id");
        }
    }
}
