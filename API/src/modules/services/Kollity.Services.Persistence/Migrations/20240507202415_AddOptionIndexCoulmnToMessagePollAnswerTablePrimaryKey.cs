using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOptionIndexCoulmnToMessagePollAnswerTablePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_message_poll_answer",
                schema: "services",
                table: "MessagePollAnswer");

            migrationBuilder.AlterColumn<string>(
                name: "poll",
                schema: "services",
                table: "RoomMessage",
                type: "nvarchar(3600)",
                maxLength: 3600,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3550)",
                oldMaxLength: 3550,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_message_poll_answer",
                schema: "services",
                table: "MessagePollAnswer",
                columns: new[] { "poll_id", "user_id", "option_index" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_message_poll_answer",
                schema: "services",
                table: "MessagePollAnswer");

            migrationBuilder.AlterColumn<string>(
                name: "poll",
                schema: "services",
                table: "RoomMessage",
                type: "nvarchar(3550)",
                maxLength: 3550,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3600)",
                oldMaxLength: 3600,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_message_poll_answer",
                schema: "services",
                table: "MessagePollAnswer",
                columns: new[] { "poll_id", "user_id" });
        }
    }
}
