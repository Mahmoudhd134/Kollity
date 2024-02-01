using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    profile_image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    type = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    full_name_in_arabic = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    user_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "bit", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    security_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "bit", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "bit", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "bit", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claim_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    department = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    hours = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_asp_net_users_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claim_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    provider_key = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    provider_display_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_login", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_login_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    user_agent = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_refresh_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    login_provider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_token", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_token_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseAssistant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assistant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_assistant", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_assistant_asp_net_users_assistant_id",
                        column: x => x.assistant_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_course_assistant_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(227)", maxLength: 227, nullable: false),
                    image = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ensure_join_request = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_course_course_id",
                        column: x => x.course_id,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_user_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_course_course_course_id",
                        column: x => x.course_id,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_student_course_user_student_id",
                        column: x => x.student_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Assigment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 4095, nullable: false),
                    mode = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assigment", x => x.id);
                    table.ForeignKey(
                        name: "fk_assigment_asp_net_users_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_assigment_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_group", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_group_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
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
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomMessage",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", maxLength: 4067, nullable: true),
                    file = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sender_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_message", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_message_room_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_message_user_sender_id",
                        column: x => x.sender_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RoomSupervisor",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supervisor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_supervisor", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_supervisor_room_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_supervisor_user_supervisor_id",
                        column: x => x.supervisor_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoom",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    room_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    join_request_accepted = table.Column<bool>(type: "bit", nullable: false),
                    last_online_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_room", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_room_room_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_room_user_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AssignmentImage",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    image = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_image_assigment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "Assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentAnswer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    upload_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    file = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    assignment_group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_answer_asp_net_users_student_id",
                        column: x => x.student_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_assignment_answer_assignment_groups_assignment_group_id",
                        column: x => x.assignment_group_id,
                        principalTable: "AssignmentGroup",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_assignment_answer_assignments_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "Assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentGroupStudent",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assignment_group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    join_request_accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_group_student", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_group_student_asp_net_users_student_id",
                        column: x => x.student_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignment_group_student_assignment_group_assignment_group_id",
                        column: x => x.assignment_group_id,
                        principalTable: "AssignmentGroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    open_for_seconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_question_exam_exam_id",
                        column: x => x.exam_id,
                        principalTable: "Exam",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestionOption",
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
                        principalTable: "ExamQuestion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAnswer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exam_question_option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    request_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    submit_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exam_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_answer_asp_net_users_student_id",
                        column: x => x.student_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                        column: x => x.exam_question_option_id,
                        principalTable: "ExamQuestionOption",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exam_answer_exam_questions_exam_question_id",
                        column: x => x.exam_question_id,
                        principalTable: "ExamQuestion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_exam_answer_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "Exam",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_assigment_doctor_id",
                table: "Assigment",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_assigment_room_id",
                table: "Assigment",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_assignment_group_id",
                table: "AssignmentAnswer",
                column: "assignment_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_assignment_id",
                table: "AssignmentAnswer",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_answer_student_id",
                table: "AssignmentAnswer",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_code",
                table: "AssignmentGroup",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_room_id",
                table: "AssignmentGroup",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_student_assignment_group_id",
                table: "AssignmentGroupStudent",
                column: "assignment_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_student_student_id",
                table: "AssignmentGroupStudent",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_image_assignment_id",
                table: "AssignmentImage",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_code",
                table: "Course",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_course_doctor_id",
                table: "Course",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_assistant_assistant_id",
                table: "CourseAssistant",
                column: "assistant_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_assistant_course_id",
                table: "CourseAssistant",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_assistant_course_id_assistant_id",
                table: "CourseAssistant",
                columns: new[] { "course_id", "assistant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_exam_room_id",
                table: "Exam",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_id",
                table: "ExamAnswer",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_id",
                table: "ExamAnswer",
                column: "exam_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_option_id",
                table: "ExamAnswer",
                column: "exam_question_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                table: "ExamAnswer",
                columns: new[] { "student_id", "exam_question_id" },
                unique: true,
                filter: "[student_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_exam_id",
                table: "ExamQuestion",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_option_exam_question_id",
                table: "ExamQuestionOption",
                column: "exam_question_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "normalized_name",
                unique: true,
                filter: "[normalized_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_role_claim_role_id",
                table: "RoleClaim",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_course_id",
                table: "Room",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_doctor_id",
                table: "Room",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_message_room_id",
                table: "RoomMessage",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_message_sender_id",
                table: "RoomMessage",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_supervisor_room_id_supervisor_id",
                table: "RoomSupervisor",
                columns: new[] { "room_id", "supervisor_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_room_supervisor_supervisor_id",
                table: "RoomSupervisor",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_course_id",
                table: "StudentCourse",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_student_id",
                table: "StudentCourse",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "normalized_email",
                unique: true,
                filter: "[normalized_email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_code",
                table: "User",
                column: "code",
                unique: true,
                filter: "[code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "User",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "normalized_user_name",
                unique: true,
                filter: "[normalized_user_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_claim_user_id",
                table: "UserClaim",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_login_user_id",
                table: "UserLogin",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_refresh_token_user_id",
                table: "UserRefreshToken",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_refresh_token_user_id_user_agent",
                table: "UserRefreshToken",
                columns: new[] { "user_id", "user_agent" },
                unique: true,
                filter: "[user_agent] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_role_id",
                table: "UserRole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_room_room_id",
                table: "UserRoom",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_room_user_id_room_id",
                table: "UserRoom",
                columns: new[] { "user_id", "room_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAnswer");

            migrationBuilder.DropTable(
                name: "AssignmentGroupStudent");

            migrationBuilder.DropTable(
                name: "AssignmentImage");

            migrationBuilder.DropTable(
                name: "CourseAssistant");

            migrationBuilder.DropTable(
                name: "ExamAnswer");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "RoomMessage");

            migrationBuilder.DropTable(
                name: "RoomSupervisor");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserRoom");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "AssignmentGroup");

            migrationBuilder.DropTable(
                name: "Assigment");

            migrationBuilder.DropTable(
                name: "ExamQuestionOption");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
