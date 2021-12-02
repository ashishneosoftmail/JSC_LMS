using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class GallaryTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventTableId",
                table: "Gallary");

            migrationBuilder.RenameColumn(
                name: "EventCoordinator",
                table: "Gallary",
                newName: "FileName");

            migrationBuilder.AlterColumn<int>(
                name: "EventsTableId",
                table: "Gallary",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Gallary",
                newName: "EventCoordinator");

            migrationBuilder.AlterColumn<int>(
                name: "EventsTableId",
                table: "Gallary",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EventTableId",
                table: "Gallary",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
