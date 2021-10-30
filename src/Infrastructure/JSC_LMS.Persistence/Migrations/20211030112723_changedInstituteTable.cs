using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class changedInstituteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institute_Cities_CityId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_Cities_CityId1",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_States_StateId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_States_StateId1",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_ZipCode_ZipId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_ZipCode_ZipId1",
                table: "Institute");

            migrationBuilder.DropIndex(
                name: "IX_Institute_CityId1",
                table: "Institute");

            migrationBuilder.DropIndex(
                name: "IX_Institute_StateId1",
                table: "Institute");

            migrationBuilder.DropIndex(
                name: "IX_Institute_ZipId1",
                table: "Institute");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "Institute");

            migrationBuilder.DropColumn(
                name: "StateId1",
                table: "Institute");

            migrationBuilder.DropColumn(
                name: "ZipId1",
                table: "Institute");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "Institute",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Institute",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Institute",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_Cities_CityId",
                table: "Institute",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_States_StateId",
                table: "Institute",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_ZipCode_ZipId",
                table: "Institute",
                column: "ZipId",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institute_Cities_CityId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_States_StateId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_ZipCode_ZipId",
                table: "Institute");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "Institute",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Institute",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Institute",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId1",
                table: "Institute",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId1",
                table: "Institute",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipId1",
                table: "Institute",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institute_CityId1",
                table: "Institute",
                column: "CityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_StateId1",
                table: "Institute",
                column: "StateId1");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ZipId1",
                table: "Institute",
                column: "ZipId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_Cities_CityId",
                table: "Institute",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_Cities_CityId1",
                table: "Institute",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_States_StateId",
                table: "Institute",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_States_StateId1",
                table: "Institute",
                column: "StateId1",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_ZipCode_ZipId",
                table: "Institute",
                column: "ZipId",
                principalTable: "ZipCode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_ZipCode_ZipId1",
                table: "Institute",
                column: "ZipId1",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
