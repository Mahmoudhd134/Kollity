using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAssginmentAnswerDegreeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentAnswerDegree",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    answer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    degree = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_answer_degree", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_answer_degree_asp_net_users_student_id",
                        column: x => x.student_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_assignment_answer_degree_assignment_answer_answer_id",
                        column: x => x.answer_id,
                        principalTable: "AssignmentAnswer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignment_answer_degree_assignment_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "AssignmentGroup",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_degree_answer_id",
                table: "AssignmentAnswerDegree",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_degree_group_id",
                table: "AssignmentAnswerDegree",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_degree_student_id",
                table: "AssignmentAnswerDegree",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAnswerDegree");
        }
    }
}
