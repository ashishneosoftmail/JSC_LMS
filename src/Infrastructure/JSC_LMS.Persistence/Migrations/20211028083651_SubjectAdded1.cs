using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class SubjectAdded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Class_SectionId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Section_SectionId1",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SectionId1",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "SectionId1",
                table: "Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ClassId",
                table: "Subject",
                column: "ClassId");

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
                name: "FK_Subject_Class_ClassId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Section_SectionId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_ClassId",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "SectionId1",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SectionId1",
                table: "Subject",
                column: "SectionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Class_SectionId",
                table: "Subject",
                column: "SectionId",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Section_SectionId1",
                table: "Subject",
                column: "SectionId1",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
