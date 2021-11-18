using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Identity.Migrations
{
    public partial class TemporaryPasswordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e3206c2-511f-469a-8990-5ed8e1f83bbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e5a6657-f923-4bc2-a18f-1b56eb35978f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e826999-f006-40ad-b0d7-a9f9365d47c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95cc5458-ea84-4b4b-af82-47a20f8feb64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2a14d58-c200-4703-99c5-42ca883fc94b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e46657ca-9121-437d-b05a-cc71d95710db");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "868606d2-7345-412d-81ce-eba14996a270", "cd3009fe-5103-4d47-9d96-1aca0c2d6c8d", "Parent", "PARENT" },
                    { "48400dfd-cfc6-4a29-9c30-f2a0c5aa04de", "22399f2d-b6db-4b74-995f-d7e7d171bb3a", "Student", "STUDENT" },
                    { "bc5ee135-1221-4133-a46b-f5ae0006ba0e", "6eda52d3-6671-4e8e-b0ec-8b7b8ceb0d5b", "Teacher", "TEACHER" },
                    { "f02708b9-da65-431b-ae4c-7be8b315db00", "6278426c-976c-4ca0-b8f9-17b7625a6297", "Principal", "PRINCIPAL" },
                    { "088a171e-970c-4f81-bbe0-63c6c302dd4e", "4a449581-4cec-4e21-9f9e-c55be1e1ac45", "Institute Admin", "INSTITUTE ADMIN" },
                    { "d18b7707-6597-4161-92cd-43331024c244", "11134160-bc4c-49db-8069-f93fd721d264", "Super administrator", "SUPER ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "088a171e-970c-4f81-bbe0-63c6c302dd4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48400dfd-cfc6-4a29-9c30-f2a0c5aa04de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "868606d2-7345-412d-81ce-eba14996a270");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc5ee135-1221-4133-a46b-f5ae0006ba0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d18b7707-6597-4161-92cd-43331024c244");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f02708b9-da65-431b-ae4c-7be8b315db00");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8e826999-f006-40ad-b0d7-a9f9365d47c2", "13ade9cd-886f-4a8d-bbee-8b566c5be699", "Parent", "PARENT" },
                    { "3e3206c2-511f-469a-8990-5ed8e1f83bbf", "204801f0-75ad-4929-ab80-c5e7f404a2cf", "Student", "STUDENT" },
                    { "95cc5458-ea84-4b4b-af82-47a20f8feb64", "92646cf5-130d-4551-aef4-4950fa628b5f", "Teacher", "TEACHER" },
                    { "a2a14d58-c200-4703-99c5-42ca883fc94b", "c717d32c-43be-4b74-9374-2c76787f9136", "Principal", "PRINCIPAL" },
                    { "e46657ca-9121-437d-b05a-cc71d95710db", "89770189-b8c8-4289-a19d-1932beadaee0", "Institute Admin", "INSTITUTE ADMIN" },
                    { "6e5a6657-f923-4bc2-a18f-1b56eb35978f", "7ad81327-0650-4428-8846-53b6a83006da", "Super administrator", "SUPER ADMINISTRATOR" }
                });
        }
    }
}
