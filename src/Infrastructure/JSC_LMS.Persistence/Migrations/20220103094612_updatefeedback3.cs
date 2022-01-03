using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class updatefeedback3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
              name: "SchoolId",
              table: "Feedbacks",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int",
              oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "SchoolId",
            table: "Feedbacks",
            type: "int",
            nullable: true,
            oldClrType: typeof(int));
        }
    }
}
