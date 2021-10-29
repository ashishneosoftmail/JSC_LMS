using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class Relationship2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "School",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Institute",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_School_CityId",
                table: "School",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_CityId",
                table: "Institute",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_Cities_CityId",
                table: "Institute",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Cities_CityId",
                table: "School",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institute_Cities_CityId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Cities_CityId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_CityId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_Institute_CityId",
                table: "Institute");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "School",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Institute",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
