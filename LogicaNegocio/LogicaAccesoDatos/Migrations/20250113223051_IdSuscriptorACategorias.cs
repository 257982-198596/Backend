using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class IdSuscriptorACategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuscriptorId",
                table: "Categorias",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_SuscriptorId",
                table: "Categorias",
                column: "SuscriptorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Suscriptores_SuscriptorId",
                table: "Categorias",
                column: "SuscriptorId",
                principalTable: "Suscriptores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Suscriptores_SuscriptorId",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_SuscriptorId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "SuscriptorId",
                table: "Categorias");
        }
    }
}
