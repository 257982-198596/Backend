using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class SeAgreganDBSETFrecuenciasMonedasEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_Moneda_MonedaDelCobroId",
                table: "CobroRecibido");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moneda",
                table: "Moneda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Frecuencia",
                table: "Frecuencia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoServicioDelCliente",
                table: "EstadoServicioDelCliente");

            migrationBuilder.RenameTable(
                name: "Moneda",
                newName: "Monedas");

            migrationBuilder.RenameTable(
                name: "Frecuencia",
                newName: "Frecuencias");

            migrationBuilder.RenameTable(
                name: "EstadoServicioDelCliente",
                newName: "EstadosServiciosDelClientes");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Monedas",
                table: "Monedas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Frecuencias",
                table: "Frecuencias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosServiciosDelClientes",
                table: "EstadosServiciosDelClientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido",
                column: "MonedaDelCobroId",
                principalTable: "Monedas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Clientes_ClienteId",
                table: "ServiciosDelCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_EstadosServiciosDelClientes_EstadoDelServicioDelClienteId",
                table: "ServiciosDelCliente",
                column: "EstadoDelServicioDelClienteId",
                principalTable: "EstadosServiciosDelClientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Frecuencias_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente",
                column: "FrecuenciaDelServicioId",
                principalTable: "Frecuencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Monedas_MonedaDelServicioId",
                table: "ServiciosDelCliente",
                column: "MonedaDelServicioId",
                principalTable: "Monedas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Clientes_ClienteId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_EstadosServiciosDelClientes_EstadoDelServicioDelClienteId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Frecuencias_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Monedas_MonedaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Monedas",
                table: "Monedas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Frecuencias",
                table: "Frecuencias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosServiciosDelClientes",
                table: "EstadosServiciosDelClientes");

            migrationBuilder.RenameTable(
                name: "Monedas",
                newName: "Moneda");

            migrationBuilder.RenameTable(
                name: "Frecuencias",
                newName: "Frecuencia");

            migrationBuilder.RenameTable(
                name: "EstadosServiciosDelClientes",
                newName: "EstadoServicioDelCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moneda",
                table: "Moneda",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Frecuencia",
                table: "Frecuencia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoServicioDelCliente",
                table: "EstadoServicioDelCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_Moneda_MonedaDelCobroId",
                table: "CobroRecibido",
                column: "MonedaDelCobroId",
                principalTable: "Moneda",
                principalColumn: "Id");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Moneda_MonedaDelServicioId",
                table: "ServiciosDelCliente",
                column: "MonedaDelServicioId",
                principalTable: "Moneda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
