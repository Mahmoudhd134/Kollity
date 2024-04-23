using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Reporting.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditPrimaryKeyOfCourseDoctorAndAssistantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_users_doctor_id",
                schema: "reporting",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_users_student_id",
                schema: "reporting",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_users_student_id",
                schema: "reporting",
                table: "AssignmentGroup");

            migrationBuilder.DropForeignKey(
                name: "fk_course_doctor_and_assistant_users_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant");

            migrationBuilder.DropForeignKey(
                name: "fk_course_student_users_student_id",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_users_doctor_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_users_student_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_room_users_doctor_id",
                schema: "reporting",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "pk_course_doctor_and_assistant",
                schema: "reporting",
                table: "CourseDoctorAndAssistant");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_course_doctor_and_assistant",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_course_doctor_and_assistant_course_id_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                columns: new[] { "course_id", "doctor_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_doctors_doctor_id",
                schema: "reporting",
                table: "Assignment",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_students_student_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_students_student_id",
                schema: "reporting",
                table: "AssignmentGroup",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_course_doctor_and_assistant_doctors_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_course_student_students_student_id",
                schema: "reporting",
                table: "CourseStudent",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_doctors_doctor_id",
                schema: "reporting",
                table: "Exam",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_students_student_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_room_doctors_doctor_id",
                schema: "reporting",
                table: "Room",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_doctors_doctor_id",
                schema: "reporting",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_students_student_id",
                schema: "reporting",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_students_student_id",
                schema: "reporting",
                table: "AssignmentGroup");

            migrationBuilder.DropForeignKey(
                name: "fk_course_doctor_and_assistant_doctors_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant");

            migrationBuilder.DropForeignKey(
                name: "fk_course_student_students_student_id",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_doctors_doctor_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_students_student_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_room_doctors_doctor_id",
                schema: "reporting",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "pk_course_doctor_and_assistant",
                schema: "reporting",
                table: "CourseDoctorAndAssistant");

            migrationBuilder.DropIndex(
                name: "ix_course_doctor_and_assistant_course_id_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant");

            migrationBuilder.AddPrimaryKey(
                name: "pk_course_doctor_and_assistant",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                columns: new[] { "course_id", "doctor_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_users_doctor_id",
                schema: "reporting",
                table: "Assignment",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_users_student_id",
                schema: "reporting",
                table: "AssignmentAnswer",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_users_student_id",
                schema: "reporting",
                table: "AssignmentGroup",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_course_doctor_and_assistant_users_doctor_id",
                schema: "reporting",
                table: "CourseDoctorAndAssistant",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_course_student_users_student_id",
                schema: "reporting",
                table: "CourseStudent",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_users_doctor_id",
                schema: "reporting",
                table: "Exam",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_users_student_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "student_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_room_users_doctor_id",
                schema: "reporting",
                table: "Room",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");
        }
    }
}
