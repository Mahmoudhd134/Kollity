using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Reporting.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomIdColumnToAssignmentAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "room_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_room_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_rooms_room_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                column: "room_id",
                principalSchema: "reporting",
                principalTable: "Room",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_rooms_room_id",
                schema: "reporting",
                table: "AssignmentAnswer");

            migrationBuilder.DropIndex(
                name: "ix_assignment_answer_room_id",
                schema: "reporting",
                table: "AssignmentAnswer");

            migrationBuilder.DropColumn(
                name: "room_id",
                schema: "reporting",
                table: "AssignmentAnswer");
        }
    }
}
