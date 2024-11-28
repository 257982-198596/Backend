using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class ServicioDelClienteAEntidadCobro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicioDelClienteId",
                table: "CobrosRecibidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CobrosRecibidos_ServicioDelClienteId",
                table: "CobrosRecibidos",
                column: "ServicioDelClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CobrosRecibidos_ServiciosDelCliente_ServicioDelClienteId",
                table: "CobrosRecibidos",
                column: "ServicioDelClienteId",
                principalTable: "ServiciosDelCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction 
    );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobrosRecibidos_ServiciosDelCliente_ServicioDelClienteId",
                table: "CobrosRecibidos");

            migrationBuilder.DropIndex(
                name: "IX_CobrosRecibidos_ServicioDelClienteId",
                table: "CobrosRecibidos");

            migrationBuilder.DropColumn(
                name: "ServicioDelClienteId",
                table: "CobrosRecibidos");
        }
    }
}
