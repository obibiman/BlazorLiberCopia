using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibliographia.Web.API.Migrations
{
    public partial class AddingRolesto_usersSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e312ad81-8e51-4256-a660-df740d8c5c88", "7032ed52-8880-4138-9375-64f50efaa5e5", "User", "USER" },
                    { "e4866a3d-ee37-498a-b68c-3aa641cb51f5", "9d216c1f-fffb-4646-bf17-c3236334067f", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "64701336-a647-426a-afab-ff3efb23a443", 0, "853df6d3-f969-4a6a-9452-f461ab928738", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAECFyxx7IliTCIOcbR07ZuUuYFQcN2pURFQuus73E/JH9O8PBZwMbykyE1+13mkuIug==", null, false, "45aa894d-a534-44bb-bcf1-311771f1b383", false, "admin@bookstore.com" },
                    { "7f3a7775-8726-4b1e-b3e1-2dd2e3a162b9", 0, "abc38e3d-dbe3-4523-bd26-801e077e70cb", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEJAT6ceVissDVLabHANYKLZW9SgkyN3ax3gKSexEqRfGeRrVvzNIAXplWPFAGBwX2A==", null, false, "e83eaa98-5cca-43ff-a869-8ed02d2cc441", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e4866a3d-ee37-498a-b68c-3aa641cb51f5", "64701336-a647-426a-afab-ff3efb23a443" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e312ad81-8e51-4256-a660-df740d8c5c88", "7f3a7775-8726-4b1e-b3e1-2dd2e3a162b9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e4866a3d-ee37-498a-b68c-3aa641cb51f5", "64701336-a647-426a-afab-ff3efb23a443" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e312ad81-8e51-4256-a660-df740d8c5c88", "7f3a7775-8726-4b1e-b3e1-2dd2e3a162b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e312ad81-8e51-4256-a660-df740d8c5c88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4866a3d-ee37-498a-b68c-3aa641cb51f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64701336-a647-426a-afab-ff3efb23a443");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f3a7775-8726-4b1e-b3e1-2dd2e3a162b9");
        }
    }
}
