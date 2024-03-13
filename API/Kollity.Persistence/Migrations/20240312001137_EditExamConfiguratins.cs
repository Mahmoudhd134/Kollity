using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditExamConfiguratins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exam_questions_exam_question_id",
                table: "ExamAnswer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "submit_time",
                table: "ExamAnswer",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "exam_question_option_id",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                table: "ExamAnswer",
                column: "exam_question_option_id",
                principalTable: "ExamQuestionOption",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exam_questions_exam_question_id",
                table: "ExamAnswer",
                column: "exam_question_id",
                principalTable: "ExamQuestion",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                table: "ExamAnswer");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_answer_exam_questions_exam_question_id",
                table: "ExamAnswer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "submit_time",
                table: "ExamAnswer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "exam_question_option_id",
                table: "ExamAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exam_question_options_exam_question_option_id",
                table: "ExamAnswer",
                column: "exam_question_option_id",
                principalTable: "ExamQuestionOption",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_exam_answer_exam_questions_exam_question_id",
                table: "ExamAnswer",
                column: "exam_question_id",
                principalTable: "ExamQuestion",
                principalColumn: "id");
        }
    }
}
