using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDegreeColumnToExamQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "degree",
                table: "ExamQuestion",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "degree",
                table: "ExamQuestion");
        }
    }
}
