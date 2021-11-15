using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class Academic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academic",
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CutOff = table.Column<decimal>(type: "decimal(18,7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Academic_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Academic_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Academic_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Academic_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Academic_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academic_ClassId",
                table: "Academic",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_SchoolId",
                table: "Academic",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_SectionId",
                table: "Academic",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_SubjectId",
                table: "Academic",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Academic_TeacherId",
                table: "Academic",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academic");
        }
    }
}
