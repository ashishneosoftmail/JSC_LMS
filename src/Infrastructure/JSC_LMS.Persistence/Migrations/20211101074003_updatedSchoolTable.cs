using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class updatedSchoolTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Cities_CityId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_States_StateId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_ZipCode_ZipId",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "School",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "School",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "School",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "School",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Cities_CityId",
                table: "School",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School",
                column: "InstituteId",
                principalTable: "Institute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_School_States_StateId",
                table: "School",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_School_ZipCode_ZipId",
                table: "School",
                column: "ZipId",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Cities_CityId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_States_StateId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_ZipCode_ZipId",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "School",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "School",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "School",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "School",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Cities_CityId",
                table: "School",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Institute_InstituteId",
                table: "School",
                column: "InstituteId",
                principalTable: "Institute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_School_States_StateId",
                table: "School",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_School_ZipCode_ZipId",
                table: "School",
                column: "ZipId",
                principalTable: "ZipCode",
                principalColumn: "Id");
        }
    }
}
