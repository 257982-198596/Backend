using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class cobrosRecibidosABaseDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_Clientes_ClienteId",
                table: "CobroRecibido");

            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_MediosDePago_MedioPagoId",
                table: "CobroRecibido");

            migrationBuilder.DropForeignKey(
                name: "FK_CobroRecibido_Monedas_MonedaDelCobroId",
                table: "CobroRecibido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CobroRecibido",
                table: "CobroRecibido");

            migrationBuilder.RenameTable(
                name: "CobroRecibido",
                newName: "CobrosRecibidos");

            migrationBuilder.RenameIndex(
                name: "IX_CobroRecibido_MonedaDelCobroId",
                table: "CobrosRecibidos",
                newName: "IX_CobrosRecibidos_MonedaDelCobroId");

            migrationBuilder.RenameIndex(
                name: "IX_CobroRecibido_MedioPagoId",
                table: "CobrosRecibidos",
                newName: "IX_CobrosRecibidos_MedioPagoId");

            migrationBuilder.RenameIndex(
                name: "IX_CobroRecibido_ClienteId",
                table: "CobrosRecibidos",
                newName: "IX_CobrosRecibidos_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CobrosRecibidos",
                table: "CobrosRecibidos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobrosRecibidos_Clientes_ClienteId",
                table: "CobrosRecibidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobrosRecibidos_MediosDePago_MedioPagoId",
                table: "CobrosRecibidos",
                column: "MedioPagoId",
                principalTable: "MediosDePago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CobrosRecibidos_Monedas_MonedaDelCobroId",
                table: "CobrosRecibidos",
                column: "MonedaDelCobroId",
                principalTable: "Monedas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobrosRecibidos_Clientes_ClienteId",
                table: "CobrosRecibidos");

            migrationBuilder.DropForeignKey(
                name: "FK_CobrosRecibidos_MediosDePago_MedioPagoId",
                table: "CobrosRecibidos");

            migrationBuilder.DropForeignKey(
                name: "FK_CobrosRecibidos_Monedas_MonedaDelCobroId",
                table: "CobrosRecibidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CobrosRecibidos",
                table: "CobrosRecibidos");

            migrationBuilder.RenameTable(
                name: "CobrosRecibidos",
                newName: "CobroRecibido");

            migrationBuilder.RenameIndex(
                name: "IX_CobrosRecibidos_MonedaDelCobroId",
                table: "CobroRecibido",
                newName: "IX_CobroRecibido_MonedaDelCobroId");

            migrationBuilder.RenameIndex(
                name: "IX_CobrosRecibidos_MedioPagoId",
                table: "CobroRecibido",
                newName: "IX_CobroRecibido_MedioPagoId");

            migrationBuilder.RenameIndex(
                name: "IX_CobrosRecibidos_ClienteId",
                table: "CobroRecibido",
                newName: "IX_CobroRecibido_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CobroRecibido",
                table: "CobroRecibido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CobroRecibido_Clientes_ClienteId",
                table: "CobroRecibido",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

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
    }
}
