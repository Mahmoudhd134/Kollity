using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExamTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamAnswer",
                schema: "services");

            migrationBuilder.DropTable(
                name: "ExamQuestionOption",
                schema: "services");

            migrationBuilder.DropTable(
                name: "ExamQuestion",
                schema: "services");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "services",
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    degree = table.Column<byte>(type: "tinyint", nullable: false),
                    open_for_seconds = table.Column<int>(type: "int", nullable: false),
                    question = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_question_exam_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "services",
                        principalTable: "Exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestionOption",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_right_option = table.Column<bool>(type: "bit", nullable: false),
                    option = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_question_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_question_option_exam_question_exam_question_id",
                        column: x => x.exam_question_id,
                        principalSchema: "services",
                        principalTable: "ExamQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAnswer",
                schema: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    request_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    submit_time = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                        column: x => x.exam_question_option_id,
                        principalSchema: "services",
                        principalTable: "ExamQuestionOption",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_exam_questions_exam_question_id",
                        column: x => x.exam_question_id,
                        principalSchema: "services",
                        principalTable: "ExamQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exam_answer_exams_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "services",
                        principalTable: "Exam",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_students_student_id",
                        column: x => x.student_id,
                        principalSchema: "services",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exam_room_id",
                schema: "services",
                table: "Exam",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_id",
                schema: "services",
                table: "ExamAnswer",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_id",
                schema: "services",
                table: "ExamAnswer",
                column: "exam_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_option_id",
                schema: "services",
                table: "ExamAnswer",
                column: "exam_question_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                schema: "services",
                table: "ExamAnswer",
                columns: new[] { "student_id", "exam_question_id" },
                unique: true,
                filter: "[student_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_exam_id",
                schema: "services",
                table: "ExamQuestion",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_option_exam_question_id",
                schema: "services",
                table: "ExamQuestionOption",
                column: "exam_question_id");
        }
    }
}
