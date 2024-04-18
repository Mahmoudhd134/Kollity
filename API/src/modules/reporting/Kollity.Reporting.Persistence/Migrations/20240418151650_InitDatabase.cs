using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Reporting.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reporting");

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "reporting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    department = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "reporting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name_in_arabic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    profile_image = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    type = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    doctor_type = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CourseDoctorAndAssistant",
                schema: "reporting",
                columns: table => new
                {
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_doctor = table.Column<bool>(type: "bit", nullable: false),
                    assigning_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_currently_assigned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_doctor_and_assistant", x => new { x.course_id, x.doctor_id });
                    table.ForeignKey(
                        name: "fk_course_doctor_and_assistant_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "reporting",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_course_doctor_and_assistant_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                schema: "reporting",
                columns: table => new
                {
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_student", x => new { x.course_id, x.student_id });
                    table.ForeignKey(
                        name: "fk_course_student_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "reporting",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_course_student_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "reporting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(227)", maxLength: 227, nullable: false),
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
                        principalSchema: "reporting",
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_room_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                schema: "reporting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 4095, nullable: false),
                    mode = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)20),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_update_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    open_until_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "reporting",
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_assignment_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroup",
                schema: "reporting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_group", x => new { x.id, x.student_id });
                    table.ForeignKey(
                        name: "fk_assignment_group_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "reporting",
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_assignment_group_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "reporting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    question_text = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    question_open_for_seconds = table.Column<int>(type: "int", nullable: true),
                    question_degree = table.Column<byte>(type: "tinyint", nullable: true),
                    option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    option = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    is_right_option = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "reporting",
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_users_doctor_id",
                        column: x => x.doctor_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RoomUser",
                schema: "reporting",
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
                        principalSchema: "reporting",
                        principalTable: "Room",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_room_user_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AssignmentAnswer",
                schema: "reporting",
                columns: table => new
                {
                    assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    degree = table.Column<int>(type: "int", nullable: true),
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_answer", x => new { x.student_id, x.assignment_id });
                    table.ForeignKey(
                        name: "fk_assignment_answer_assignment_groups_group_id_student_id",
                        columns: x => new { x.group_id, x.student_id },
                        principalSchema: "reporting",
                        principalTable: "AssignmentGroup",
                        principalColumns: new[] { "id", "student_id" });
                    table.ForeignKey(
                        name: "fk_assignment_answer_assignments_assignment_id",
                        column: x => x.assignment_id,
                        principalSchema: "reporting",
                        principalTable: "Assignment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_assignment_answer_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ExamAnswer",
                schema: "reporting",
                columns: table => new
                {
                    option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    request_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    submit_time = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_answer", x => new { x.option_id, x.student_id });
                    table.ForeignKey(
                        name: "fk_exam_answer_exams_option_id",
                        column: x => x.option_id,
                        principalSchema: "reporting",
                        principalTable: "Exam",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_users_student_id",
                        column: x => x.student_id,
                        principalSchema: "reporting",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                schema: "reporting",
                table: "User",
                columns: new[] { "id", "doctor_type", "email", "full_name_in_arabic", "is_deleted", "profile_image", "type", "user_name" },
                values: new object[] { new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"), 1, "nassermahmoud571@gmail.com", "Mahmoud Ahmed Nasser Mahmoud", false, null, "Doctor", "Mahmoudhd134" });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_doctor_id",
                schema: "reporting",
                table: "Assignment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_room_id",
                schema: "reporting",
                table: "Assignment",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_assignment_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_group_id_student_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                columns: new[] { "group_id", "student_id" });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_code",
                schema: "reporting",
                table: "AssignmentGroup",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_room_id",
                schema: "reporting",
                table: "AssignmentGroup",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_student_id",
                schema: "reporting",
                table: "AssignmentGroup",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_code",
                schema: "reporting",
                table: "Course",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_course_doctor_and_assistant_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_student_student_id",
                schema: "reporting",
                table: "CourseStudent",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_doctor_id",
                schema: "reporting",
                table: "Exam",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_exam_id_question_id_option_id",
                schema: "reporting",
                table: "Exam",
                columns: new[] { "exam_id", "question_id", "option_id" });

            migrationBuilder.CreateIndex(
                name: "ix_exam_option_id",
                schema: "reporting",
                table: "Exam",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_id",
                schema: "reporting",
                table: "Exam",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_room_id",
                schema: "reporting",
                table: "Exam",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_course_id",
                schema: "reporting",
                table: "Room",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_doctor_id",
                schema: "reporting",
                table: "Room",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_user_user_id",
                schema: "reporting",
                table: "RoomUser",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                schema: "reporting",
                table: "User",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_user_name",
                schema: "reporting",
                table: "User",
                column: "user_name",
                unique: true,
                filter: "[user_name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAnswer",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "CourseDoctorAndAssistant",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "CourseStudent",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "ExamAnswer",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "RoomUser",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "AssignmentGroup",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "Assignment",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "Course",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "User",
                schema: "reporting");
        }
    }
}
