using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoomSupervisorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomSupervisor");

            migrationBuilder.AddColumn<bool>(
                name: "is_supervisor",
                table: "UserRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "ix_user_room_is_supervisor",
                table: "UserRoom",
                column: "is_supervisor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_room_is_supervisor",
                table: "UserRoom");

            migrationBuilder.DropColumn(
                name: "is_supervisor",
                table: "UserRoom");

            migrationBuilder.CreateTable(
                name: "RoomSupervisor",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supervisor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_supervisor", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_supervisor_room_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_supervisor_user_supervisor_id",
                        column: x => x.supervisor_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_room_supervisor_room_id_supervisor_id",
                table: "RoomSupervisor",
                columns: new[] { "room_id", "supervisor_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_room_supervisor_supervisor_id",
                table: "RoomSupervisor",
                column: "supervisor_id");
        }
    }
}
