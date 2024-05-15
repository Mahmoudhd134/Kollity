using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Services.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "services");

            migrationBuilder.RenameTable(
                name: "UserRoom",
                newName: "UserRoom",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "StudentCourse",
                newName: "StudentCourse",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "RoomMessage",
                newName: "RoomMessage",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "RoomContent",
                newName: "RoomContent",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Room",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "OutboxMessage",
                newName: "OutboxMessage",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "MessagePollAnswer",
                newName: "MessagePollAnswer",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "ExamQuestionOption",
                newName: "ExamQuestionOption",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "ExamQuestion",
                newName: "ExamQuestion",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "ExamAnswer",
                newName: "ExamAnswer",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "Exam",
                newName: "Exam",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "CourseAssistant",
                newName: "CourseAssistant",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Course",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "AssignmentGroupStudent",
                newName: "AssignmentGroupStudent",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "AssignmentGroup",
                newName: "AssignmentGroup",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "AssignmentFile",
                newName: "AssignmentFile",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "AssignmentAnswerDegree",
                newName: "AssignmentAnswerDegree",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "AssignmentAnswer",
                newName: "AssignmentAnswer",
                newSchema: "services");

            migrationBuilder.RenameTable(
                name: "Assigment",
                newName: "Assigment",
                newSchema: "services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserRoom",
                schema: "services",
                newName: "UserRoom");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "services",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "StudentCourse",
                schema: "services",
                newName: "StudentCourse");

            migrationBuilder.RenameTable(
                name: "RoomMessage",
                schema: "services",
                newName: "RoomMessage");

            migrationBuilder.RenameTable(
                name: "RoomContent",
                schema: "services",
                newName: "RoomContent");

            migrationBuilder.RenameTable(
                name: "Room",
                schema: "services",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "OutboxMessage",
                schema: "services",
                newName: "OutboxMessage");

            migrationBuilder.RenameTable(
                name: "MessagePollAnswer",
                schema: "services",
                newName: "MessagePollAnswer");

            migrationBuilder.RenameTable(
                name: "ExamQuestionOption",
                schema: "services",
                newName: "ExamQuestionOption");

            migrationBuilder.RenameTable(
                name: "ExamQuestion",
                schema: "services",
                newName: "ExamQuestion");

            migrationBuilder.RenameTable(
                name: "ExamAnswer",
                schema: "services",
                newName: "ExamAnswer");

            migrationBuilder.RenameTable(
                name: "Exam",
                schema: "services",
                newName: "Exam");

            migrationBuilder.RenameTable(
                name: "CourseAssistant",
                schema: "services",
                newName: "CourseAssistant");

            migrationBuilder.RenameTable(
                name: "Course",
                schema: "services",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "AssignmentGroupStudent",
                schema: "services",
                newName: "AssignmentGroupStudent");

            migrationBuilder.RenameTable(
                name: "AssignmentGroup",
                schema: "services",
                newName: "AssignmentGroup");

            migrationBuilder.RenameTable(
                name: "AssignmentFile",
                schema: "services",
                newName: "AssignmentFile");

            migrationBuilder.RenameTable(
                name: "AssignmentAnswerDegree",
                schema: "services",
                newName: "AssignmentAnswerDegree");

            migrationBuilder.RenameTable(
                name: "AssignmentAnswer",
                schema: "services",
                newName: "AssignmentAnswer");

            migrationBuilder.RenameTable(
                name: "Assigment",
                schema: "services",
                newName: "Assigment");
        }
    }
}
