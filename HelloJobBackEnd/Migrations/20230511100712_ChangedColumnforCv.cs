using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class ChangedColumnforCv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_Educations_EducationId",
                table: "Cvs");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Cvs");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Cvs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_Educations_EducationId",
                table: "Cvs",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_Educations_EducationId",
                table: "Cvs");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Cvs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_Educations_EducationId",
                table: "Cvs",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id");
        }
    }
}
