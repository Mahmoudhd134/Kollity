using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Feedback.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "feedback");

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    department = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackQuestion",
                schema: "feedback",
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
                name: "User",
                schema: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    user_type = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                schema: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_student", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_student_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "feedback",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_course_student_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "feedback",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "feedback",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_room_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "feedback",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "feedback",
                        principalTable: "Room",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RoomUser",
                schema: "feedback",
                columns: table => new
                {
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_user", x => new { x.room_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_room_user_room_room_id",
                        column: x => x.room_id,
                        principalSchema: "feedback",
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_room_user_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "feedback",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FeedbackAnswer",
                schema: "feedback",
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
                        principalSchema: "feedback",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_exam_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "feedback",
                        principalTable: "Exam",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_feedback_questions_question_id",
                        column: x => x.question_id,
                        principalSchema: "feedback",
                        principalTable: "FeedbackQuestion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "feedback",
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_feedback_answer_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "feedback",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_code",
                schema: "feedback",
                table: "Course",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_course_student_course_id_student_id",
                schema: "feedback",
                table: "CourseStudent",
                columns: new[] { "course_id", "student_id" });

            migrationBuilder.CreateIndex(
                name: "ix_course_student_student_id",
                schema: "feedback",
                table: "CourseStudent",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_room_id",
                schema: "feedback",
                table: "Exam",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_course_id",
                schema: "feedback",
                table: "FeedbackAnswer",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_doctor_id",
                schema: "feedback",
                table: "FeedbackAnswer",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_exam_id",
                schema: "feedback",
                table: "FeedbackAnswer",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_question_id",
                schema: "feedback",
                table: "FeedbackAnswer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedback_answer_student_id",
                schema: "feedback",
                table: "FeedbackAnswer",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_course_id",
                schema: "feedback",
                table: "Room",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_doctor_id",
                schema: "feedback",
                table: "Room",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_user_user_id",
                schema: "feedback",
                table: "RoomUser",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudent",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "FeedbackAnswer",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "RoomUser",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "FeedbackQuestion",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "Course",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "User",
                schema: "feedback");
        }
    }
}
