using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "SuscriptorId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes",
                column: "SuscriptorId",
                principalTable: "Suscriptores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "SuscriptorId",
                table: "Clientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Suscriptores_SuscriptorId",
                table: "Clientes",
                column: "SuscriptorId",
                principalTable: "Suscriptores",
                principalColumn: "Id");
        }
    }
}
