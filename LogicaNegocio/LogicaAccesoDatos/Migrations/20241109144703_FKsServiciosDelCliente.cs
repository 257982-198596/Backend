using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class FKsServiciosDelCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Frecuencia_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Moneda_MonedaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Servicios_ServicioContratadoId",
                table: "ServiciosDelCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ServicioContratadoId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MonedaDelServicioId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FrecuenciaDelServicioId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosDelCliente_Servicios_ServicioContratadoId",
                table: "ServiciosDelCliente",
                column: "ServicioContratadoId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Frecuencia_FrecuenciaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Moneda_MonedaDelServicioId",
                table: "ServiciosDelCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosDelCliente_Servicios_ServicioContratadoId",
                table: "ServiciosDelCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ServicioContratadoId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MonedaDelServicioId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FrecuenciaDelServicioId",
                table: "ServiciosDelCliente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
