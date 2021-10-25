using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class updatingTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "States",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Roles",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Cities",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Categories",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "States",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Roles",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Cities",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Categories",
                newName: "isActive");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Cities",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Cities",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
