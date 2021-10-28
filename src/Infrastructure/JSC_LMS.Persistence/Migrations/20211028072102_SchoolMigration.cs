using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class SchoolMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    InstituteId = table.Column<int>(nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    ZipId = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_School_Institute_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_School_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_School_ZipCode_ZipId",
                        column: x => x.ZipId,
                        principalTable: "ZipCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_CityId",
                table: "School",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_School_InstituteId",
                table: "School",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_School_StateId",
                table: "School",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_School_ZipId",
                table: "School",
                column: "ZipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "School");
        }
    }
}
