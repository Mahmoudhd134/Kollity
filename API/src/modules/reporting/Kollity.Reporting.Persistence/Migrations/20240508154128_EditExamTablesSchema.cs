using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Reporting.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditExamTablesSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exam_doctors_doctor_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exams_option_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exam_answer",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_student_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_doctor_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "ix_exam_exam_id_question_id_option_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "ix_exam_option_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "ix_exam_question_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "doctor_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "exam_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "is_right_option",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "option",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "option_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "question_degree",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "question_id",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "question_open_for_seconds",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "question_text",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.RenameColumn(
                name: "option_id",
                schema: "reporting",
                table: "ExamAnswer",
                newName: "room_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "submit_time",
                schema: "reporting",
                table: "ExamAnswer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "reporting",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "exam_id",
                schema: "reporting",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "exam_question_id",
                schema: "reporting",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "exam_question_option_id",
                schema: "reporting",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "reporting",
                table: "Exam",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_updated_date",
                schema: "reporting",
                table: "Exam",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "pk_exam_answer",
                schema: "reporting",
                table: "ExamAnswer",
                column: "id");

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                schema: "reporting",
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
                        principalSchema: "reporting",
                        principalTable: "Exam",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestionOption",
                schema: "reporting",
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
                        principalSchema: "reporting",
                        principalTable: "ExamQuestion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "exam_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_exam_question_option_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "exam_question_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_room_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                schema: "reporting",
                table: "ExamAnswer",
                columns: new[] { "student_id", "exam_question_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_exam_id",
                schema: "reporting",
                table: "ExamQuestion",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_exam_question_option_exam_question_id",
                schema: "reporting",
                table: "ExamQuestionOption",
                column: "exam_question_id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exam_questions_exam_question_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "exam_question_id",
                principalSchema: "reporting",
                principalTable: "ExamQuestion",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exams_exam_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "exam_id",
                principalSchema: "reporting",
                principalTable: "Exam",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exams_question_options_exam_question_option_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "exam_question_option_id",
                principalSchema: "reporting",
                principalTable: "ExamQuestionOption",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_rooms_room_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "room_id",
                principalSchema: "reporting",
                principalTable: "Room",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exam_questions_exam_question_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exams_exam_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exams_question_options_exam_question_option_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_rooms_room_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropTable(
                name: "ExamQuestionOption",
                schema: "reporting");

            migrationBuilder.DropTable(
                name: "ExamQuestion",
                schema: "reporting");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exam_answer",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_exam_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_exam_question_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_exam_question_option_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_room_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropIndex(
                name: "ix_exam_answer_student_id_exam_question_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropColumn(
                name: "exam_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropColumn(
                name: "exam_question_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropColumn(
                name: "exam_question_option_id",
                schema: "reporting",
                table: "ExamAnswer");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "last_updated_date",
                schema: "reporting",
                table: "Exam");

            migrationBuilder.RenameColumn(
                name: "room_id",
                schema: "reporting",
                table: "ExamAnswer",
                newName: "option_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "submit_time",
                schema: "reporting",
                table: "ExamAnswer",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "doctor_id",
                schema: "reporting",
                table: "Exam",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "exam_id",
                schema: "reporting",
                table: "Exam",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "is_right_option",
                schema: "reporting",
                table: "Exam",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "option",
                schema: "reporting",
                table: "Exam",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "option_id",
                schema: "reporting",
                table: "Exam",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "question_degree",
                schema: "reporting",
                table: "Exam",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "question_id",
                schema: "reporting",
                table: "Exam",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "question_open_for_seconds",
                schema: "reporting",
                table: "Exam",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "question_text",
                schema: "reporting",
                table: "Exam",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_exam_answer",
                schema: "reporting",
                table: "ExamAnswer",
                columns: new[] { "option_id", "student_id" });

            migrationBuilder.CreateIndex(
                name: "ix_exam_answer_student_id",
                schema: "reporting",
                table: "ExamAnswer",
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

            migrationBuilder.AddForeignKey(
                name: "fk_exam_doctors_doctor_id",
                schema: "reporting",
                table: "Exam",
                column: "doctor_id",
                principalSchema: "reporting",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exams_option_id",
                schema: "reporting",
                table: "ExamAnswer",
                column: "option_id",
                principalSchema: "reporting",
                principalTable: "Exam",
                principalColumn: "id");
        }
    }
}
