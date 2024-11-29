using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class mediosDePagoABD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_MedioDePago_MedioPagoId",
                table: "CobroRecibido");

            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedioDePago",
                table: "MedioDePago");

            migrationBuilder.RenameTable(
                name: "MedioDePago",
                newName: "MediosDePago");

            migrationBuilder.AlterColumn<int>(
                name: "MonedaDelCobroId",
                table: "CobroRecibido",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedioPagoId",
                table: "CobroRecibido",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SuscriptorId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediosDePago",
                table: "MediosDePago",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes",
                column: "SuscriptorId",
                principalTable: "Suscriptores",
                principalColumn: "Id",
                onDelete:ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_MediosDePago_MedioPagoId",
                table: "CobroRecibido",
                column: "MedioPagoId",
                principalTable: "MediosDePago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido",
                column: "MonedaDelCobroId",
                principalTable: "Monedas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_MediosDePago_MedioPagoId",
                table: "CobroRecibido");

            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediosDePago",
                table: "MediosDePago");

            migrationBuilder.RenameTable(
                name: "MediosDePago",
                newName: "MedioDePago");

            migrationBuilder.AlterColumn<int>(
                name: "MonedaDelCobroId",
                table: "CobroRecibido",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MedioPagoId",
                table: "CobroRecibido",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SuscriptorId",
                table: "Clientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedioDePago",
                table: "MedioDePago",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes",
                column: "SuscriptorId",
                principalTable: "Suscriptores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_MedioDePago_MedioPagoId",
                table: "CobroRecibido",
                column: "MedioPagoId",
                principalTable: "MedioDePago",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido",
                column: "MonedaDelCobroId",
                principalTable: "Monedas",
                principalColumn: "Id");
        }
    }
}
