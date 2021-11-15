using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class KnowledgeBaseTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBase_CategoryId",
                table: "KnowledgeBase",
                column: "CategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeBase_Categories_CategoryId",
                table: "KnowledgeBase",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeBase_Categories_CategoryId",
                table: "KnowledgeBase");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeBase_CategoryId",
                table: "KnowledgeBase");
        }
    }
}
