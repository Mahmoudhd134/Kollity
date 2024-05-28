using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CheckForUdates___TEMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assigment_users_doctor_id",
                schema: "services",
                table: "Assigment");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_users_student_id",
                schema: "services",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_degree_users_student_id",
                schema: "services",
                table: "AssignmentAnswerDegree");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_student_users_student_id",
                schema: "services",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_course_users_doctor_id",
                schema: "services",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "fk_course_assistant_users_assistant_id",
                schema: "services",
                table: "CourseAssistant");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_users_student_id",
                schema: "services",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_room_user_doctor_id",
                schema: "services",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "fk_student_course_user_student_id",
                schema: "services",
                table: "StudentCourse");

            migrationBuilder.AddForeignKey(
                name: "fk_assigment_doctors_doctor_id",
                schema: "services",
                table: "Assigment",
                column: "doctor_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_students_student_id",
                schema: "services",
                table: "AssignmentAnswer",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_degree_students_student_id",
                schema: "services",
                table: "AssignmentAnswerDegree",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_student_students_student_id",
                schema: "services",
                table: "AssignmentGroupStudent",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_course_doctors_doctor_id",
                schema: "services",
                table: "Course",
                column: "doctor_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_course_assistant_doctors_assistant_id",
                schema: "services",
                table: "CourseAssistant",
                column: "assistant_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_students_student_id",
                schema: "services",
                table: "ExamAnswer",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_room_doctors_doctor_id",
                schema: "services",
                table: "Room",
                column: "doctor_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_student_course_students_student_id",
                schema: "services",
                table: "StudentCourse",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assigment_doctors_doctor_id",
                schema: "services",
                table: "Assigment");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_students_student_id",
                schema: "services",
                table: "AssignmentAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_answer_degree_students_student_id",
                schema: "services",
                table: "AssignmentAnswerDegree");

            migrationBuilder.DropForeignKey(
                name: "fk_assignment_group_student_students_student_id",
                schema: "services",
                table: "AssignmentGroupStudent");

            migrationBuilder.DropForeignKey(
                name: "fk_course_doctors_doctor_id",
                schema: "services",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "fk_course_assistant_doctors_assistant_id",
                schema: "services",
                table: "CourseAssistant");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_students_student_id",
                schema: "services",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_room_doctors_doctor_id",
                schema: "services",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "fk_student_course_students_student_id",
                schema: "services",
                table: "StudentCourse");

            migrationBuilder.AddForeignKey(
                name: "fk_assigment_users_doctor_id",
                schema: "services",
                table: "Assigment",
                column: "doctor_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_users_student_id",
                schema: "services",
                table: "AssignmentAnswer",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_answer_degree_users_student_id",
                schema: "services",
                table: "AssignmentAnswerDegree",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_group_student_users_student_id",
                schema: "services",
                table: "AssignmentGroupStudent",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_course_users_doctor_id",
                schema: "services",
                table: "Course",
                column: "doctor_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_course_assistant_users_assistant_id",
                schema: "services",
                table: "CourseAssistant",
                column: "assistant_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_users_student_id",
                schema: "services",
                table: "ExamAnswer",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_room_user_doctor_id",
                schema: "services",
                table: "Room",
                column: "doctor_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_student_course_user_student_id",
                schema: "services",
                table: "StudentCourse",
                column: "student_id",
                principalSchema: "services",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
