using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Identity.Migrations
{
    public partial class addingSecondStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "66e46492-812f-4f31-aa85-9970f31c936b", "e6d2bfb5-edb8-414e-83a7-b5d034ea0eba", "Parent", "PARENT" },
                    { "0a7a6779-28c8-404c-be97-0d246900ca3b", "03fa751f-6e65-4eff-9776-be2774f1acf9", "Student", "STUDENT" },
                    { "5083d129-84fe-4624-b1dd-dbe82857d0e3", "341bece9-12ca-4b50-abbb-fda885c6b025", "Teacher", "TEACHER" },
                    { "d84c73c3-4ae6-49d9-9578-08b217869324", "6fb9cc4b-8b44-4f27-bf51-820ee327ccd3", "Principal", "PRINCIPAL" },
                    { "1d8e78ba-e705-425f-9c7e-7c3ebbd621be", "864ded63-c8d0-4002-92e2-16387034cf54", "Institute Admin", "INSTITUTE ADMIN" },
                    { "ba042edf-e30e-404a-97d9-1cb2e6d7cda6", "4a477858-94e8-42d0-a276-810bc72f8c9c", "Super administrator", "SUPER ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a7a6779-28c8-404c-be97-0d246900ca3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d8e78ba-e705-425f-9c7e-7c3ebbd621be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5083d129-84fe-4624-b1dd-dbe82857d0e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66e46492-812f-4f31-aa85-9970f31c936b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba042edf-e30e-404a-97d9-1cb2e6d7cda6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d84c73c3-4ae6-49d9-9578-08b217869324");

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
    }
}
