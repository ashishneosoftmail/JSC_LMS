using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class InstituteSchoolRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "School",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_School_InstituteId",
                table: "School",
                column: "InstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School",
                column: "InstituteId",
                principalTable: "Institute",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_InstituteId",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "School",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
