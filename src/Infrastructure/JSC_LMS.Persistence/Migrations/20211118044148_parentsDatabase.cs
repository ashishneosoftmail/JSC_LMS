using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class parentsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parents",
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
                    UserType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    ZipId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parents_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parents_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parents_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parents_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parents_ZipCode_ZipId",
                        column: x => x.ZipId,
                        principalTable: "ZipCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_CityId",
                table: "Parents",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ClassId",
                table: "Parents",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_SectionId",
                table: "Parents",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_StateId",
                table: "Parents",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ZipId",
                table: "Parents",
                column: "ZipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parents");
        }
    }
}
