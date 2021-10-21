using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Practica.Migrations
{
    public partial class migration20102021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionCorta",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionCorta",
                table: "Productos");
        }
    }
}
