using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Persistence.Migrations
{
    public partial class addedCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "T_LM_Categories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "T_LM_Categories",
                newName: "CategoryName");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Events",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "T_LM_Categories",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                table: "T_LM_Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "T_LM_Categories",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "T_LM_Categories",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "T_LM_Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "T_LM_Categories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_LM_Categories",
                table: "T_LM_Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId1",
                table: "Events",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_T_LM_Categories_CategoryId1",
                table: "Events",
                column: "CategoryId1",
                principalTable: "T_LM_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_T_LM_Categories_CategoryId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId1",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_LM_Categories",
                table: "T_LM_Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "T_LM_Categories");

            migrationBuilder.RenameTable(
                name: "T_LM_Categories",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
