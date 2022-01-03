using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class updatefeedback2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
              name: "SchoolId",
              table: "Feedbacks",
              nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "Feedbacks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

           
            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_SchoolId",
                table: "Feedbacks",
                column: "SchoolId");


            

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_School_SchoolId",
                table: "Feedbacks",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Class_ClassId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_School_SchoolId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Section_SectionId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ClassId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_SchoolId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_SectionId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "Feedbacks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
