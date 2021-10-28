using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class ClassAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
