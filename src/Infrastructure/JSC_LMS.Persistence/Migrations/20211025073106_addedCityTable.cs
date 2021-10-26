using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class addedCityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_LM_Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    CityName = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: false),
                    Logitude = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LM_Cities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LM_Cities");
        }
    }
}
