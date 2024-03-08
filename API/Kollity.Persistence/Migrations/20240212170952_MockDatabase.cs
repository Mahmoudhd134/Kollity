using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MockDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { new Guid("126abefb-6d50-4d58-9419-c8e1f39a01d8"), null, "Doctor", "DOCTOR" },
                    { new Guid("6ddc2275-7ae1-40ca-9f6f-c5b5c637c5d8"), null, "Assistant", "ASSISTANT" },
                    { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), null, "Admin", "ADMIN" },
                    { new Guid("bf9c94d0-ca32-4b64-aa5a-3c03b44db740"), null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "profile_image", "security_stamp", "two_factor_enabled", "type", "user_name" },
                values: new object[] { new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"), 0, "a443bf96-da75-4046-8452-7d64553b4533", "nassermahmoud571@gmail.com", false, true, null, "NASSERMAHMOUD571@GMAIL.COM", "MAHMOUDHD134", "AQAAAAIAAYagAAAAEPRFyxksWTOaY3gzYwnqUGS8FT0q1kCjlaUo1KP/Uu3R1seoxDWoi1tlyw8Uc69YNA==", null, false, null, "6TPMB3KY7R4NAIGXTMKLOWGRE2HQOOBY", false, "Doctor", "Mahmoudhd134" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "id",
                keyValue: new Guid("126abefb-6d50-4d58-9419-c8e1f39a01d8"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "id",
                keyValue: new Guid("6ddc2275-7ae1-40ca-9f6f-c5b5c637c5d8"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "id",
                keyValue: new Guid("bf9c94d0-ca32-4b64-aa5a-3c03b44db740"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa") });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "id",
                keyValue: new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "id",
                keyValue: new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"));
        }
    }
}
