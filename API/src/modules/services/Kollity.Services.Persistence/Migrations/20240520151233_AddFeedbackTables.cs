using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackQuestion",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    is_mcq = table.Column<bool>(type: "bit", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_feedback_question", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackAnswer",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    answer = table.Column<int>(type: "int", nullable: true),
                    string_answer = table.Column<string>(type: "nvarchar(max)", maxLength: 4095, nullable: true),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_feedback_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_feedback_answer_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "services",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "services",
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_exam_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "services",
                        principalTable: "Exam",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_feedback_question_question_id",
                        column: x => x.question_id,
                        principalSchema: "services",
                        principalTable: "FeedbackQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_feedback_answer_students_student_id",
                        column: x => x.student_id,
                        principalSchema: "services",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_course_id",
                schema: "services",
                table: "FeedbackAnswer",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_doctor_id",
                schema: "services",
                table: "FeedbackAnswer",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_exam_id",
                schema: "services",
                table: "FeedbackAnswer",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_question_id",
                schema: "services",
                table: "FeedbackAnswer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_student_id",
                schema: "services",
                table: "FeedbackAnswer",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackAnswer",
                schema: "services");

            migrationBuilder.DropTable(
                name: "FeedbackQuestion",
                schema: "services");
        }
    }
}
