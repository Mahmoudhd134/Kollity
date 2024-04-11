using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.User.API.Migrations
{
    /// <inheritdoc />
    public partial class MockDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImage", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"), 0, "a443bf96-da75-4046-8452-7d64553b4533", "nassermahmoud571@gmail.com", false, true, null, "NASSERMAHMOUD571@GMAIL.COM", "MAHMOUDHD134", "AQAAAAIAAYagAAAAEPRFyxksWTOaY3gzYwnqUGS8FT0q1kCjlaUo1KP/Uu3R1seoxDWoi1tlyw8Uc69YNA==", null, false, null, "6TPMB3KY7R4NAIGXTMKLOWGRE2HQOOBY", false, "Mahmoudhd134" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa") });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"));
        }
    }
}
