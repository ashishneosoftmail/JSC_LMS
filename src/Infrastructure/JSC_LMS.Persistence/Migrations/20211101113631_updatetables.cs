using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Cities_CityId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Class_ClassId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_School_SchoolId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Section_SectionId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_States_StateId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Subject_SubjectId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_ZipCode_ZipId",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "Teacher",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Teacher",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Teacher",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Cities_CityId",
                table: "Teacher",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Class_ClassId",
                table: "Teacher",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_School_SchoolId",
                table: "Teacher",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Section_SectionId",
                table: "Teacher",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_States_StateId",
                table: "Teacher",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Subject_SubjectId",
                table: "Teacher",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_ZipCode_ZipId",
                table: "Teacher",
                column: "ZipId",
                principalTable: "ZipCode",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Cities_CityId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Class_ClassId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_School_SchoolId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Section_SectionId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_States_StateId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Subject_SubjectId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_ZipCode_ZipId",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "ZipId",
                table: "Teacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Teacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Teacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Cities_CityId",
                table: "Teacher",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Class_ClassId",
                table: "Teacher",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_School_SchoolId",
                table: "Teacher",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Section_SectionId",
                table: "Teacher",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_States_StateId",
                table: "Teacher",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Subject_SubjectId",
                table: "Teacher",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_ZipCode_ZipId",
                table: "Teacher",
                column: "ZipId",
                principalTable: "ZipCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
