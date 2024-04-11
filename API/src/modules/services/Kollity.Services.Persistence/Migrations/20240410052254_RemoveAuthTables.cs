using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuthTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assigment_asp_net_users_doctor_id",
                table: "Assigment");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_degree_asp_net_users_student_id",
                table: "AssignmentAnswerDegree");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_student_asp_net_users_student_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_course_asp_net_users_doctor_id",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "fk_course_assistant_asp_net_users_assistant_id",
                table: "CourseAssistant");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_asp_net_users_student_id",
                table: "ExamAnswer");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "User");

            migrationBuilder.DropColumn(
                name: "concurrency_stamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "email_confirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "lockout_enabled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "lockout_end",
                table: "User");

            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "User");

            migrationBuilder.DropColumn(
                name: "phone_number_confirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "security_stamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "two_factor_enabled",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                table: "User",
                newName: "user_type");

            migrationBuilder.RenameIndex(
                name: "EmailIndex",
                table: "User",
                newName: "ix_user_normalized_email");

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_user_name",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_email",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_assigment_users_doctor_id",
                table: "Assigment",
                column: "doctor_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_users_student_id",
                table: "AssignmentAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_degree_users_student_id",
                table: "AssignmentAnswerDegree",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_student_users_student_id",
                table: "AssignmentGroupStudent",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_course_users_doctor_id",
                table: "Course",
                column: "doctor_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_course_assistant_users_assistant_id",
                table: "CourseAssistant",
                column: "assistant_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_users_student_id",
                table: "ExamAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assigment_users_doctor_id",
                table: "Assigment");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_users_student_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_degree_users_student_id",
                table: "AssignmentAnswerDegree");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_student_users_student_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_course_users_doctor_id",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "fk_course_assistant_users_assistant_id",
                table: "CourseAssistant");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_users_student_id",
                table: "ExamAnswer");

            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "User",
                newName: "access_failed_count");

            migrationBuilder.RenameIndex(
                name: "ix_user_normalized_email",
                table: "User",
                newName: "EmailIndex");

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_user_name",
                table: "User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_email",
                table: "User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "concurrency_stamp",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "email_confirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "lockout_enabled",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "lockout_end",
                table: "User",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "User",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "phone_number_confirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "security_stamp",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "two_factor_enabled",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    refresh_token = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    user_agent = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
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
                name: "RoleClaim",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { new Guid("126abefb-6d50-4d58-9419-c8e1f39a01d8"), null, "Doctor", "DOCTOR" },
                    { new Guid("6ddc2275-7ae1-40ca-9f6f-c5b5c637c5d8"), null, "Assistant", "ASSISTANT" },
                    { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), null, "Admin", "ADMIN" },
                    { new Guid("bf9c94d0-ca32-4b64-aa5a-3c03b44db740"), null, "Student", "STUDENT" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"),
                columns: new[] { "concurrency_stamp", "email_confirmed", "lockout_enabled", "lockout_end", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled" },
                values: new object[] { "a443bf96-da75-4046-8452-7d64553b4533", false, true, null, "AQAAAAIAAYagAAAAEPRFyxksWTOaY3gzYwnqUGS8FT0q1kCjlaUo1KP/Uu3R1seoxDWoi1tlyw8Uc69YNA==", null, false, "6TPMB3KY7R4NAIGXTMKLOWGRE2HQOOBY", false });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { new Guid("be2a5cab-0ae7-4335-8316-4154a5cfa35f"), new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa") });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "normalized_user_name",
                unique: true,
                filter: "[normalized_user_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_role_name",
                table: "Role",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

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

            migrationBuilder.AddForeignKey(
                name: "fk_assigment_asp_net_users_doctor_id",
                table: "Assigment",
                column: "doctor_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_degree_asp_net_users_student_id",
                table: "AssignmentAnswerDegree",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_student_asp_net_users_student_id",
                table: "AssignmentGroupStudent",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_course_asp_net_users_doctor_id",
                table: "Course",
                column: "doctor_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_course_assistant_asp_net_users_assistant_id",
                table: "CourseAssistant",
                column: "assistant_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_asp_net_users_student_id",
                table: "ExamAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
