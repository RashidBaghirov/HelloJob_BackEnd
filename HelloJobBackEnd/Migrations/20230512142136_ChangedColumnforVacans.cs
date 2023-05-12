using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    public partial class ChangedColumnforVacans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    OperatingModeId = table.Column<int>(type: "int", nullable: false),
                    ExperienceId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    BusinessAreaId = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    DrivingLicense = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacans_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vacans_BusinessArea_BusinessAreaId",
                        column: x => x.BusinessAreaId,
                        principalTable: "BusinessArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacans_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacans_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacans_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacans_OperatingModes_OperatingModeId",
                        column: x => x.OperatingModeId,
                        principalTable: "OperatingModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoEmployeers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VacansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoEmployeers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoEmployeers_Vacans_VacansId",
                        column: x => x.VacansId,
                        principalTable: "Vacans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VacansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoWorks_Vacans_VacansId",
                        column: x => x.VacansId,
                        principalTable: "Vacans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoEmployeers_VacansId",
                table: "InfoEmployeers",
                column: "VacansId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoWorks_VacansId",
                table: "InfoWorks",
                column: "VacansId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_BusinessAreaId",
                table: "Vacans",
                column: "BusinessAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_CityId",
                table: "Vacans",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_CompanyId",
                table: "Vacans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_EducationId",
                table: "Vacans",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_ExperienceId",
                table: "Vacans",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_OperatingModeId",
                table: "Vacans",
                column: "OperatingModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacans_UserId",
                table: "Vacans",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoEmployeers");

            migrationBuilder.DropTable(
                name: "InfoWorks");

            migrationBuilder.DropTable(
                name: "Vacans");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
