using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditModelsAndConfigureDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_assignment_groups_assignment_group_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_courses_course_id",
                table: "AssignmentGroup");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_student_courses_course_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_course_course_id",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_asp_net_users_student_id",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_question_option_exam_exam_id",
                table: "ExamQuestionOption");

            migrationBuilder.DropForeignKey(
                name: "fk_room_message_course_course_id",
                table: "RoomMessage");

            migrationBuilder.DropForeignKey(
                name: "fk_room_message_user_sender_id",
                table: "RoomMessage");

            migrationBuilder.DropForeignKey(
                name: "fk_room_supervisor_course_course_id",
                table: "RoomSupervisor");

            migrationBuilder.DropIndex(
                name: "ix_room_supervisor_course_id",
                table: "RoomSupervisor");

            migrationBuilder.DropIndex(
                name: "ix_room_message_course_id",
                table: "RoomMessage");

            migrationBuilder.DropIndex(
                name: "ix_exam_question_option_exam_id",
                table: "ExamQuestionOption");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_course_id",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "ix_assignment_group_student_course_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropIndex(
                name: "ix_assignment_group_course_id",
                table: "AssignmentGroup");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "RoomSupervisor");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "RoomMessage");

            migrationBuilder.DropColumn(
                name: "exam_id",
                table: "ExamQuestionOption");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "AssignmentGroup");

            migrationBuilder.AlterColumn<Guid>(
                name: "sender_id",
                table: "RoomMessage",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "assignment_group_id",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                table: "ExamAnswer",
                columns: new[] { "student_id", "exam_question_id" },
                unique: true,
                filter: "[student_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_assignment_groups_assignment_group_id",
                table: "AssignmentAnswer",
                column: "assignment_group_id",
                principalTable: "AssignmentGroup",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_asp_net_users_student_id",
                table: "ExamAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_room_message_user_sender_id",
                table: "RoomMessage",
                column: "sender_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_assignment_groups_assignment_group_id",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_asp_net_users_student_id",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_room_message_user_sender_id",
                table: "RoomMessage");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                table: "ExamAnswer");

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "RoomSupervisor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "sender_id",
                table: "RoomMessage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "RoomMessage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "exam_id",
                table: "ExamQuestionOption",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "Exam",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "AssignmentGroupStudent",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "AssignmentGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "assignment_group_id",
                table: "AssignmentAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_room_supervisor_course_id",
                table: "RoomSupervisor",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_message_course_id",
                table: "RoomMessage",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_option_exam_id",
                table: "ExamQuestionOption",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                table: "ExamAnswer",
                columns: new[] { "student_id", "exam_question_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_exam_course_id",
                table: "Exam",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_student_course_id",
                table: "AssignmentGroupStudent",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_group_course_id",
                table: "AssignmentGroup",
                column: "course_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_asp_net_users_student_id",
                table: "AssignmentAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_assignment_groups_assignment_group_id",
                table: "AssignmentAnswer",
                column: "assignment_group_id",
                principalTable: "AssignmentGroup",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_courses_course_id",
                table: "AssignmentGroup",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_student_courses_course_id",
                table: "AssignmentGroupStudent",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_course_course_id",
                table: "Exam",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_asp_net_users_student_id",
                table: "ExamAnswer",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_question_option_exam_exam_id",
                table: "ExamQuestionOption",
                column: "exam_id",
                principalTable: "Exam",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_room_message_course_course_id",
                table: "RoomMessage",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_room_message_user_sender_id",
                table: "RoomMessage",
                column: "sender_id",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_room_supervisor_course_course_id",
                table: "RoomSupervisor",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "id");
        }
    }
}
