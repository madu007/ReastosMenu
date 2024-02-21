using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReastosMenu.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePaths",
                table: "MenuImages",
                newName: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "MenuImages",
                newName: "ImagePaths");
        }
    }
}
