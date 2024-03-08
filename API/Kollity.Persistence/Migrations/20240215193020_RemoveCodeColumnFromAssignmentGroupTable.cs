using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCodeColumnFromAssignmentGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_assignment_group_code",
                table: "AssignmentGroup");

            migrationBuilder.DropColumn(
                name: "code",
                table: "AssignmentGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "code",
                table: "AssignmentGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_code",
                table: "AssignmentGroup",
                column: "code",
                unique: true);
        }
    }
}
