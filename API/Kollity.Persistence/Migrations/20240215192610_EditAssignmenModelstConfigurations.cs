using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditAssignmenModelstConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropIndex(
                name: "ix_assignment_group_student_student_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_student_student_id_assignment_group_id",
                table: "AssignmentGroupStudent",
                columns: new[] { "student_id", "assignment_group_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropIndex(
                name: "ix_assignment_group_student_student_id_assignment_group_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_student_student_id",
                table: "AssignmentGroupStudent",
                column: "student_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id");
        }
    }
}
