using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditColumnUserIdInUserRoomTableConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_room_user_user_id",
                table: "UserRoom");

            migrationBuilder.AddForeignKey(
                name: "fk_user_room_user_user_id",
                table: "UserRoom",
                column: "user_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_room_user_user_id",
                table: "UserRoom");

            migrationBuilder.AddForeignKey(
                name: "fk_user_room_user_user_id",
                table: "UserRoom",
                column: "user_id",
                principalTable: "User",
                principalColumn: "id");
        }
    }
}
