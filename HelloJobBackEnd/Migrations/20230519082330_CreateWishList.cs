using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class CreateWishList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Cvs_CvId",
                table: "WishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Vacans_VacansId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_CvId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_VacansId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "CvId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "VacansId",
                table: "WishLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CvId",
                table: "WishLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VacansId",
                table: "WishLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_CvId",
                table: "WishLists",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_VacansId",
                table: "WishLists",
                column: "VacansId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Cvs_CvId",
                table: "WishLists",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Vacans_VacansId",
                table: "WishLists",
                column: "VacansId",
                principalTable: "Vacans",
                principalColumn: "Id");
        }
    }
}
