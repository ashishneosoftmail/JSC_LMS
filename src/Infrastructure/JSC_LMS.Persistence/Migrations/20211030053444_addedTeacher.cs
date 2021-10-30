using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class addedTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrincipalId",
                table: "School",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Principal",
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
                    SchoolId = table.Column<int>(nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    ZipId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Principal_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Principal_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Principal_ZipCode_ZipId",
                        column: x => x.ZipId,
                        principalTable: "ZipCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_PrincipalId",
                table: "School",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Principal_CityId",
                table: "Principal",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Principal_StateId",
                table: "Principal",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Principal_ZipId",
                table: "Principal",
                column: "ZipId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Principal_PrincipalId",
                table: "School",
                column: "PrincipalId",
                principalTable: "Principal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Principal_PrincipalId",
                table: "School");

            migrationBuilder.DropTable(
                name: "Principal");

            migrationBuilder.DropIndex(
                name: "IX_School_PrincipalId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "PrincipalId",
                table: "School");
        }
    }
}
