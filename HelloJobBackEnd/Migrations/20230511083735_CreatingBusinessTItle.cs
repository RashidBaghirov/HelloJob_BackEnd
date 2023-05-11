using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class CreatingBusinessTItle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessTitleId",
                table: "BusinessArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BusinessTitle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTitle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessArea_BusinessTitleId",
                table: "BusinessArea",
                column: "BusinessTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessArea_BusinessTitle_BusinessTitleId",
                table: "BusinessArea",
                column: "BusinessTitleId",
                principalTable: "BusinessTitle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessArea_BusinessTitle_BusinessTitleId",
                table: "BusinessArea");

            migrationBuilder.DropTable(
                name: "BusinessTitle");

            migrationBuilder.DropIndex(
                name: "IX_BusinessArea_BusinessTitleId",
                table: "BusinessArea");

            migrationBuilder.DropColumn(
                name: "BusinessTitleId",
                table: "BusinessArea");
        }
    }
}
