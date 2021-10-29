using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subject_ClassId",
                table: "Subject",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SectionId",
                table: "Subject",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_ClassId",
                table: "Section",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolId",
                table: "Class",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Class_ClassId",
                table: "Section",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Class_ClassId",
                table: "Subject",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Section_SectionId",
                table: "Subject",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Class_ClassId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Class_ClassId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Section_SectionId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_ClassId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SectionId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Section_ClassId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolId",
                table: "Class");
        }
    }
}
