using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexUniqueToEmailColumnUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "normalized_email",
                unique: true,
                filter: "[normalized_email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "Users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "ix_users_email",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "normalized_email");
        }
    }
}
