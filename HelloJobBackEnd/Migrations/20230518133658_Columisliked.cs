using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class Columisliked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacans_AspNetUsers_UserId",
                table: "Vacans");

            migrationBuilder.DropIndex(
                name: "IX_Vacans_UserId",
                table: "Vacans");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vacans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CvId = table.Column<int>(type: "int", nullable: true),
                    VacansId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Vacans_VacansId",
                        column: x => x.VacansId,
                        principalTable: "Vacans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CvId = table.Column<int>(type: "int", nullable: true),
                    VacansId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishLists_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishLists_Vacans_VacansId",
                        column: x => x.VacansId,
                        principalTable: "Vacans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CvId = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    VacansId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItems_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestItems_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestItems_Vacans_VacansId",
                        column: x => x.VacansId,
                        principalTable: "Vacans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CvId = table.Column<int>(type: "int", nullable: true),
                    WishListId = table.Column<int>(type: "int", nullable: false),
                    VacansId = table.Column<int>(type: "int", nullable: true),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListItems_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishListItems_Vacans_VacansId",
                        column: x => x.VacansId,
                        principalTable: "Vacans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishListItems_WishLists_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_CvId",
                table: "RequestItems",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_RequestId",
                table: "RequestItems",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_VacansId",
                table: "RequestItems",
                column: "VacansId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CvId",
                table: "Requests",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId",
                table: "Requests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VacansId",
                table: "Requests",
                column: "VacansId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_CvId",
                table: "WishListItems",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_VacansId",
                table: "WishListItems",
                column: "VacansId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_WishListId",
                table: "WishListItems",
                column: "WishListId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_CvId",
                table: "WishLists",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_VacansId",
                table: "WishLists",
                column: "VacansId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestItems");

            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vacans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_UserId",
                table: "Vacans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacans_AspNetUsers_UserId",
                table: "Vacans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
