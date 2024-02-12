using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomContentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomContent",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    upload_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    uploader_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_content", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_content_room_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_content_user_uploader_id",
                        column: x => x.uploader_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_room_content_room_id",
                table: "RoomContent",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_content_uploader_id",
                table: "RoomContent",
                column: "uploader_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomContent");
        }
    }
}
