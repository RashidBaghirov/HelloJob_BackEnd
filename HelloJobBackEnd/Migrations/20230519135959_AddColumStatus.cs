using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class AddColumStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Companies");
        }
    }
}
