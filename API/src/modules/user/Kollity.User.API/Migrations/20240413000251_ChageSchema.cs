using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.User.API.Migrations
{
    /// <inheritdoc />
    public partial class ChageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "UserToken",
                newName: "UserToken",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRole",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "UserRefreshToken",
                newName: "UserRefreshToken",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "UserLogin",
                newName: "UserLogin",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "UserClaim",
                newName: "UserClaim",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                newName: "RoleClaim",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Role",
                newSchema: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserToken",
                schema: "user",
                newName: "UserToken");

            migrationBuilder.RenameTable(
                name: "UserRole",
                schema: "user",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRefreshToken",
                schema: "user",
                newName: "UserRefreshToken");

            migrationBuilder.RenameTable(
                name: "UserLogin",
                schema: "user",
                newName: "UserLogin");

            migrationBuilder.RenameTable(
                name: "UserClaim",
                schema: "user",
                newName: "UserClaim");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "user",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                schema: "user",
                newName: "RoleClaim");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "user",
                newName: "Role");
        }
    }
}
