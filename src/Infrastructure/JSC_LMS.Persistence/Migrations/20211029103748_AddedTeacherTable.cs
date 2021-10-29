using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class AddedTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teacher",
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
                    UserTypeId = table.Column<string>(nullable: true),
                    TeacherName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    ZipId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teacher_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_ZipCode_ZipId",
                        column: x => x.ZipId,
                        principalTable: "ZipCode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_CityId",
                table: "Teacher",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_ClassId",
                table: "Teacher",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_SchoolId",
                table: "Teacher",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_SectionId",
                table: "Teacher",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_StateId",
                table: "Teacher",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_SubjectId",
                table: "Teacher",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_ZipId",
                table: "Teacher",
                column: "ZipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teacher");
        }
    }
}
