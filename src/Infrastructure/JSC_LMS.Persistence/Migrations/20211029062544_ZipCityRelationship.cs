using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class ZipCityRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ZipCode_CityId",
                table: "ZipCode",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZipCode_Cities_CityId",
                table: "ZipCode",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZipCode_Cities_CityId",
                table: "ZipCode");

            migrationBuilder.DropIndex(
                name: "IX_ZipCode_CityId",
                table: "ZipCode");
        }
    }
}
