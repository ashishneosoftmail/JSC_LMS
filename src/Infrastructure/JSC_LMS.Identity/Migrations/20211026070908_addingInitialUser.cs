using Microsoft.EntityFrameworkCore.Migrations;

namespace JSC_LMS.Identity.Migrations
{
    public partial class addingInitialUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "418b7c3e-4439-42e2-ac14-38877dfd48ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a148e1df-29e5-441e-8dc3-fe4f2ae16a79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a303e5f7-6c38-4066-ae1a-c77649b17c2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aab8c143-91a2-4a36-bb30-92cad1a925ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af2d5413-4063-4c35-80da-79653fd9b0e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2fef1bb-abc7-42ca-9aac-ed5dfd6373ee");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "aab8c143-91a2-4a36-bb30-92cad1a925ee", "213fb46d-7363-4117-b773-58a5d5ea0615", "Parent", "PARENT" },
                    { "a303e5f7-6c38-4066-ae1a-c77649b17c2d", "b39f5cae-2d16-4bbd-94c3-6365e0b210f1", "Student", "STUDENT" },
                    { "af2d5413-4063-4c35-80da-79653fd9b0e4", "4ce7c4e3-4184-449d-b65d-b9b502703f12", "Teacher", "TEACHER" },
                    { "d2fef1bb-abc7-42ca-9aac-ed5dfd6373ee", "e578c7a0-346b-4ef9-bf03-25750ecd5781", "Principal", "PRINCIPAL" },
                    { "a148e1df-29e5-441e-8dc3-fe4f2ae16a79", "7c16956e-014c-4c34-ad6c-d71dac3907b5", "Institute Admin", "INSTITUTE ADMIN" },
                    { "418b7c3e-4439-42e2-ac14-38877dfd48ea", "886a3ffc-29e8-4712-abd8-4cb6d9296d8f", "Super administrator", "SUPER ADMINISTRATOR" }
                });
        }
    }
}
