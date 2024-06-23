using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Exams.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "exams");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    user_type = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(227)", maxLength: 227, nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "exams",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "exams",
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomUser",
                schema: "exams",
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
                        principalSchema: "exams",
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_user_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "exams",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                schema: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    open_for_seconds = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_question_exam_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "exams",
                        principalTable: "Exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestionOption",
                schema: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    option = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    is_right_option = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_question_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_question_option_exam_question_exam_question_id",
                        column: x => x.exam_question_id,
                        principalSchema: "exams",
                        principalTable: "ExamQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAnswer",
                schema: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    request_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    submit_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                        column: x => x.exam_question_option_id,
                        principalSchema: "exams",
                        principalTable: "ExamQuestionOption",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_exam_questions_exam_question_id",
                        column: x => x.exam_question_id,
                        principalSchema: "exams",
                        principalTable: "ExamQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exam_answer_exams_exam_id",
                        column: x => x.exam_id,
                        principalSchema: "exams",
                        principalTable: "Exam",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "exams",
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "exams",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_exam_room_id",
                schema: "exams",
                table: "Exam",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_id",
                schema: "exams",
                table: "ExamAnswer",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_id",
                schema: "exams",
                table: "ExamAnswer",
                column: "exam_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_option_id",
                schema: "exams",
                table: "ExamAnswer",
                column: "exam_question_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_room_id",
                schema: "exams",
                table: "ExamAnswer",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                schema: "exams",
                table: "ExamAnswer",
                columns: new[] { "student_id", "exam_question_id" },
                unique: true,
                filter: "[student_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_exam_id",
                schema: "exams",
                table: "ExamQuestion",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_option_exam_question_id",
                schema: "exams",
                table: "ExamQuestionOption",
                column: "exam_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_doctor_id",
                schema: "exams",
                table: "Room",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_user_user_id",
                schema: "exams",
                table: "RoomUser",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamAnswer",
                schema: "exams");

            migrationBuilder.DropTable(
                name: "RoomUser",
                schema: "exams");

            migrationBuilder.DropTable(
                name: "ExamQuestionOption",
                schema: "exams");

            migrationBuilder.DropTable(
                name: "ExamQuestion",
                schema: "exams");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "exams");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "exams");

            migrationBuilder.DropTable(
                name: "User",
                schema: "exams");
        }
    }
}
