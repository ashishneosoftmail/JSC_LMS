using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class updatetables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "School",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "School",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School",
                column: "InstituteId",
                principalTable: "Institute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "School",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School",
                column: "InstituteId",
                principalTable: "Institute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
