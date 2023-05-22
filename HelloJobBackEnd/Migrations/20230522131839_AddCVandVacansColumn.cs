using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class AddCVandVacansColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Row",
                table: "Rules");

            migrationBuilder.AddColumn<bool>(
                name: "CV",
                table: "Rules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vacans",
                table: "Rules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "Vacans",
                table: "Rules");

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
