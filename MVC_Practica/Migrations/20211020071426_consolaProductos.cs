using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Practica.Migrations
{
    public partial class consolaProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdConsolas",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdConsolas",
                table: "Productos",
                column: "IdConsolas");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Consolas_IdConsolas",
                table: "Productos",
                column: "IdConsolas",
                principalTable: "Consolas",
                principalColumn: "IdConsola",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Consolas_IdConsolas",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_IdConsolas",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "IdConsolas",
                table: "Productos");
        }
    }
}
