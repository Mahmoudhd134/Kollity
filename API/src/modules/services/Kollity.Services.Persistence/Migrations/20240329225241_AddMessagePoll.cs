using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagePoll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "file",
                table: "RoomMessage",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(511)",
                oldMaxLength: 511,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "poll",
                table: "RoomMessage",
                type: "nvarchar(3550)",
                maxLength: 3550,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MessagePollAnswer",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    poll_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    option_index = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_poll_answer", x => new { x.poll_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_message_poll_answer_room_messages_poll_id",
                        column: x => x.poll_id,
                        principalTable: "RoomMessage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_poll_answer_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_message_poll_answer_user_id",
                table: "MessagePollAnswer",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagePollAnswer");

            migrationBuilder.DropColumn(
                name: "poll",
                table: "RoomMessage");

            migrationBuilder.AlterColumn<string>(
                name: "file",
                table: "RoomMessage",
                type: "nvarchar(511)",
                maxLength: 511,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1023)",
                oldMaxLength: 1023,
                oldNullable: true);
        }
    }
}
