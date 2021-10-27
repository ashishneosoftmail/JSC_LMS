using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Identity.Migrations
{
    public partial class addingInitialStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14940564-7f61-46e5-91f6-b18b44ba264d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c9bffbb-d08f-4560-9eac-dc4b177d59fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "953fb22e-2fbb-4e68-a508-5923e93301b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a28b0577-6eb1-4cbf-9a5f-d4abf4d884e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6aa3d67-7572-436b-a5fe-baa0dbcc4d41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe5c83e4-81a5-4885-a2c8-a176332a0e8e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f95ce11-9900-4f67-b65f-e7997b77c8d4", "01449c73-6b8e-4cb2-ab58-b6d8753a4f16", "Parent", "PARENT" },
                    { "ed546225-b4a2-4aec-8f01-f0ec6c338d45", "47eba02a-e712-4527-b89b-f81f1852df92", "Student", "STUDENT" },
                    { "28388287-8a13-45af-9bdc-239711e32e91", "a9fd12f9-5901-4a2e-adb3-6e063a284cb4", "Teacher", "TEACHER" },
                    { "89f99aa9-4bc1-40ca-814e-05bcaa4d8740", "00740e48-5ac2-4683-9cec-03a05af742de", "Principal", "PRINCIPAL" },
                    { "862577cb-ba0a-4bff-a225-ae528442f96e", "349b7749-3767-4466-999f-91cd3e42376e", "Institute Admin", "INSTITUTE ADMIN" },
                    { "5f338acb-04da-468b-a77b-096287510c00", "3316a5f2-0f87-4a80-a076-582843338ee1", "Super administrator", "SUPER ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28388287-8a13-45af-9bdc-239711e32e91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f95ce11-9900-4f67-b65f-e7997b77c8d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f338acb-04da-468b-a77b-096287510c00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "862577cb-ba0a-4bff-a225-ae528442f96e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89f99aa9-4bc1-40ca-814e-05bcaa4d8740");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed546225-b4a2-4aec-8f01-f0ec6c338d45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d6aa3d67-7572-436b-a5fe-baa0dbcc4d41", "e24b9b08-deb3-49a6-bef5-f3db5800f36b", "Parent", "PARENT" },
                    { "7c9bffbb-d08f-4560-9eac-dc4b177d59fe", "4898e320-0e0c-4ed3-8f9b-73d95b0aa00a", "Student", "STUDENT" },
                    { "fe5c83e4-81a5-4885-a2c8-a176332a0e8e", "cc099008-0821-44f5-96f4-1a645347502c", "Teacher", "TEACHER" },
                    { "14940564-7f61-46e5-91f6-b18b44ba264d", "aaca54d3-a496-4c84-a574-a9c0bfc1da0a", "Principal", "PRINCIPAL" },
                    { "a28b0577-6eb1-4cbf-9a5f-d4abf4d884e0", "69782a6c-de47-4908-a868-76a94a5df14a", "Institute Admin", "INSTITUTE ADMIN" },
                    { "953fb22e-2fbb-4e68-a508-5923e93301b4", "3fda3498-3596-48d3-b4e2-7929293458a2", "Super administrator", "SUPER ADMINISTRATOR" }
                });
        }
    }
}
