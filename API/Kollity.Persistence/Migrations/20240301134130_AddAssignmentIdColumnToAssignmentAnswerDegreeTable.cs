using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentIdColumnToAssignmentAnswerDegreeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "assignment_id",
                table: "AssignmentAnswerDegree",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_degree_assignment_id",
                table: "AssignmentAnswerDegree",
                column: "assignment_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_degree_assignments_assignment_id",
                table: "AssignmentAnswerDegree",
                column: "assignment_id",
                principalTable: "Assigment",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_degree_assignments_assignment_id",
                table: "AssignmentAnswerDegree");

            migrationBuilder.DropIndex(
                name: "ix_assignment_answer_degree_assignment_id",
                table: "AssignmentAnswerDegree");

            migrationBuilder.DropColumn(
                name: "assignment_id",
                table: "AssignmentAnswerDegree");
        }
    }
}
