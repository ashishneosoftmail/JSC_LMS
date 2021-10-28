using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class InstituteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institute",
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
                    InstituteName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    ZipId = table.Column<int>(nullable: false),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime", nullable: false),
                    InstituteURL = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institute_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Institute_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Institute_ZipCode_ZipId",
                        column: x => x.ZipId,
                        principalTable: "ZipCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Institute_CityId",
                table: "Institute",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_StateId",
                table: "Institute",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ZipId",
                table: "Institute",
                column: "ZipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Institute");
        }
    }
}
