using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class Addcolumnbusinesstitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BusinessTitle",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "BusinessTitle");
        }
    }
}
