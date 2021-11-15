using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class KnowledgeBaseTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KnowledgeBase_CategoryId",
                table: "KnowledgeBase");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBase_CategoryId",
                table: "KnowledgeBase",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KnowledgeBase_CategoryId",
                table: "KnowledgeBase");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBase_CategoryId",
                table: "KnowledgeBase",
                column: "CategoryId",
                unique: true);
        }
    }
}
