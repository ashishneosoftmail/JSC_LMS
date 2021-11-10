using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class teacherUpdateeee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Teacher");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Teacher",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Teacher");

            migrationBuilder.AddColumn<string>(
                name: "UserTypeId",
                table: "Teacher",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
