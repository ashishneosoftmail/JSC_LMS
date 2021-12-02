using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class GallaryTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gallary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EventTableId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    EventsTableId = table.Column<int>(nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCoordinator = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallary_EventsTable_EventsTableId",
                        column: x => x.EventsTableId,
                        principalTable: "EventsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gallary_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gallary_EventsTableId",
                table: "Gallary",
                column: "EventsTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallary_SchoolId",
                table: "Gallary",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gallary");
        }
    }
}
