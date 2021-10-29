using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class Relationship4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "School",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "School",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "Institute",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Institute",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_School_StateId",
                table: "School",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_School_ZipId",
                table: "School",
                column: "ZipId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_StateId",
                table: "Institute",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ZipId",
                table: "Institute",
                column: "ZipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_States_StateId",
                table: "Institute",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_ZipCode_ZipId",
                table: "Institute",
                column: "ZipId",
                principalTable: "ZipCode",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_School_SchoolId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_States_StateId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_Institute_ZipCode_ZipId",
                table: "Institute");

            migrationBuilder.DropForeignKey(
                name: "FK_School_States_StateId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_School_ZipCode_ZipId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_StateId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_ZipId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_Institute_StateId",
                table: "Institute");

            migrationBuilder.DropIndex(
                name: "IX_Institute_ZipId",
                table: "Institute");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "School",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "School",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "Institute",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Institute",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
