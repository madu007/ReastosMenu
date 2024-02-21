using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReastosMenu.Migrations
{
    public partial class ImagesUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Menus",
                newName: "ImagesUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagesUrl",
                table: "Menus",
                newName: "Images");
        }
    }
}
