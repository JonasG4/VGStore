using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Practica.Migrations
{
    public partial class tbltipo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTipo",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdTipo",
                table: "Productos",
                column: "IdTipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tipo_IdTipo",
                table: "Productos",
                column: "IdTipo",
                principalTable: "Tipo",
                principalColumn: "IdTipo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tipo_IdTipo",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_IdTipo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "IdTipo",
                table: "Productos");
        }
    }
}
