using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class KnowledgeBase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Class_ClassId",
                table: "Academic");

            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Section_SectionId",
                table: "Academic");

            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Subject_SubjectId",
                table: "Academic");

            migrationBuilder.DropForeignKey(
                name: "FK_Academic_Teacher_TeacherId",
                table: "Academic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Academic",
                table: "Academic");

            migrationBuilder.RenameTable(
                name: "Academic",
                newName: "Academics");

            migrationBuilder.RenameIndex(
                name: "IX_Academic_TeacherId",
                table: "Academics",
                newName: "IX_Academics_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Academic_SubjectId",
                table: "Academics",
                newName: "IX_Academics_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Academic_SectionId",
                table: "Academics",
                newName: "IX_Academics_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Academic_ClassId",
                table: "Academics",
                newName: "IX_Academics_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Academics",
                table: "Academics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Academics_Class_ClassId",
                table: "Academics",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Academics_Section_SectionId",
                table: "Academics",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Academics_Subject_SubjectId",
                table: "Academics",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Academics_Teacher_TeacherId",
                table: "Academics",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academics_Class_ClassId",
                table: "Academics");

            migrationBuilder.DropForeignKey(
                name: "FK_Academics_Section_SectionId",
                table: "Academics");

            migrationBuilder.DropForeignKey(
                name: "FK_Academics_Subject_SubjectId",
                table: "Academics");

            migrationBuilder.DropForeignKey(
                name: "FK_Academics_Teacher_TeacherId",
                table: "Academics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Academics",
                table: "Academics");

            migrationBuilder.RenameTable(
                name: "Academics",
                newName: "Academic");

            migrationBuilder.RenameIndex(
                name: "IX_Academics_TeacherId",
                table: "Academic",
                newName: "IX_Academic_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Academics_SubjectId",
                table: "Academic",
                newName: "IX_Academic_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Academics_SectionId",
                table: "Academic",
                newName: "IX_Academic_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Academics_ClassId",
                table: "Academic",
                newName: "IX_Academic_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Academic",
                table: "Academic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Class_ClassId",
                table: "Academic",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Section_SectionId",
                table: "Academic",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Subject_SubjectId",
                table: "Academic",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Academic_Teacher_TeacherId",
                table: "Academic",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
