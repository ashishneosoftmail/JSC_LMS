using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class Relationship3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subject_SchoolId",
                table: "Subject",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_SchoolId",
                table: "Section",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_School_SchoolId",
                table: "Section",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_School_SchoolId",
                table: "Subject",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_School_SchoolId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_School_SchoolId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SchoolId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Section_SchoolId",
                table: "Section");
        }
    }
}
