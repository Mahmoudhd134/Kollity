using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxOptionsCountForSubmissionColumnToRoomMessagePoll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "poll",
                schema: "services",
                table: "RoomMessage",
                type: "nvarchar(3650)",
                maxLength: 3650,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3600)",
                oldMaxLength: 3600,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "poll",
                schema: "services",
                table: "RoomMessage",
                type: "nvarchar(3600)",
                maxLength: 3600,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3650)",
                oldMaxLength: 3650,
                oldNullable: true);
        }
    }
}
