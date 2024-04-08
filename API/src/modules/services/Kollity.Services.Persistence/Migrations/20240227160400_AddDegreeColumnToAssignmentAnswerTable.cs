using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDegreeColumnToAssignmentAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "degree",
                table: "AssignmentAnswer",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "degree",
                table: "Assigment",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)20,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "degree",
                table: "AssignmentAnswer");

            migrationBuilder.AlterColumn<byte>(
                name: "degree",
                table: "Assigment",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)20);
        }
    }
}
