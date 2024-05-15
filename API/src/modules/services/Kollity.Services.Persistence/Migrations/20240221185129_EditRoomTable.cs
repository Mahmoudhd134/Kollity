using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Room",
                type: "nvarchar(511)",
                maxLength: 511,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(511)",
                oldMaxLength: 511);

            migrationBuilder.AddColumn<byte>(
                name: "assignment_group_max_length",
                table: "Room",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "assignment_group_operations_enabled",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assignment_group_max_length",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "assignment_group_operations_enabled",
                table: "Room");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Room",
                type: "nvarchar(511)",
                maxLength: 511,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(511)",
                oldMaxLength: 511,
                oldNullable: true);
        }
    }
}
