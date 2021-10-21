using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Practica.Migrations
{
    public partial class consolaProductos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Consolas_IdConsolas",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "IdConsolas",
                table: "Productos",
                newName: "IdConsola");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_IdConsolas",
                table: "Productos",
                newName: "IX_Productos_IdConsola");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Consolas_IdConsola",
                table: "Productos",
                column: "IdConsola",
                principalTable: "Consolas",
                principalColumn: "IdConsola",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Consolas_IdConsola",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "IdConsola",
                table: "Productos",
                newName: "IdConsolas");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_IdConsola",
                table: "Productos",
                newName: "IX_Productos_IdConsolas");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Consolas_IdConsolas",
                table: "Productos",
                column: "IdConsolas",
                principalTable: "Consolas",
                principalColumn: "IdConsola",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
