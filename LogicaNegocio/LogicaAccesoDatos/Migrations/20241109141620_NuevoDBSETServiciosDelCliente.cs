using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class NuevoDBSETServiciosDelCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioDelCliente_Clientes_ClienteId",
                table: "ServicioDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicioDelCliente_EstadoServicioDelCliente_EstadoDelServicioDelClienteId",
                table: "ServicioDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicioDelCliente_Frecuencia_FrecuenciaDelServicioId",
                table: "ServicioDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicioDelCliente_Moneda_MonedaDelServicioId",
                table: "ServicioDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicioDelCliente_Servicios_ServicioContratadoId",
                table: "ServicioDelCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioDelCliente",
                table: "ServicioDelCliente");

            migrationBuilder.RenameTable(
                name: "ServicioDelCliente",
                newName: "ServiciosDelCliente");

            migrationBuilder.RenameIndex(
                name: "IX_ServicioDelCliente_ServicioContratadoId",
                table: "ServiciosDelCliente",
                newName: "IX_ServiciosDelCliente_ServicioContratadoId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicioDelCliente_MonedaDelServicioId",
                table: "ServiciosDelCliente",
                newName: "IX_ServiciosDelCliente_MonedaDelServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicioDelCliente_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente",
                newName: "IX_ServiciosDelCliente_FrecuenciaDelServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicioDelCliente_EstadoDelServicioDelClienteId",
                table: "ServiciosDelCliente",
                newName: "IX_ServiciosDelCliente_EstadoDelServicioDelClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicioDelCliente_ClienteId",
                table: "ServiciosDelCliente",
                newName: "IX_ServiciosDelCliente_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiciosDelCliente",
                table: "ServiciosDelCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Clientes_ClienteId",
                table: "ServiciosDelCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_EstadoServicioDelCliente_EstadoDelServicioDelClienteId",
                table: "ServiciosDelCliente",
                column: "EstadoDelServicioDelClienteId",
                principalTable: "EstadoServicioDelCliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Frecuencia_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente",
                column: "FrecuenciaDelServicioId",
                principalTable: "Frecuencia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Moneda_MonedaDelServicioId",
                table: "ServiciosDelCliente",
                column: "MonedaDelServicioId",
                principalTable: "Moneda",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Servicios_ServicioContratadoId",
                table: "ServiciosDelCliente",
                column: "ServicioContratadoId",
                principalTable: "Servicios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Clientes_ClienteId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_EstadoServicioDelCliente_EstadoDelServicioDelClienteId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Frecuencia_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Moneda_MonedaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Servicios_ServicioContratadoId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiciosDelCliente",
                table: "ServiciosDelCliente");

            migrationBuilder.RenameTable(
                name: "ServiciosDelCliente",
                newName: "ServicioDelCliente");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosDelCliente_ServicioContratadoId",
                table: "ServicioDelCliente",
                newName: "IX_ServicioDelCliente_ServicioContratadoId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosDelCliente_MonedaDelServicioId",
                table: "ServicioDelCliente",
                newName: "IX_ServicioDelCliente_MonedaDelServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosDelCliente_FrecuenciaDelServicioId",
                table: "ServicioDelCliente",
                newName: "IX_ServicioDelCliente_FrecuenciaDelServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosDelCliente_EstadoDelServicioDelClienteId",
                table: "ServicioDelCliente",
                newName: "IX_ServicioDelCliente_EstadoDelServicioDelClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosDelCliente_ClienteId",
                table: "ServicioDelCliente",
                newName: "IX_ServicioDelCliente_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioDelCliente",
                table: "ServicioDelCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioDelCliente_Clientes_ClienteId",
                table: "ServicioDelCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioDelCliente_EstadoServicioDelCliente_EstadoDelServicioDelClienteId",
                table: "ServicioDelCliente",
                column: "EstadoDelServicioDelClienteId",
                principalTable: "EstadoServicioDelCliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioDelCliente_Frecuencia_FrecuenciaDelServicioId",
                table: "ServicioDelCliente",
                column: "FrecuenciaDelServicioId",
                principalTable: "Frecuencia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioDelCliente_Moneda_MonedaDelServicioId",
                table: "ServicioDelCliente",
                column: "MonedaDelServicioId",
                principalTable: "Moneda",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioDelCliente_Servicios_ServicioContratadoId",
                table: "ServicioDelCliente",
                column: "ServicioContratadoId",
                principalTable: "Servicios",
                principalColumn: "Id");
        }
    }
}
