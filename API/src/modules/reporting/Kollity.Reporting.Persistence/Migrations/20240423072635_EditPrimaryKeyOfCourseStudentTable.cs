using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Reporting.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditPrimaryKeyOfCourseStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_course_student",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "reporting",
                table: "CourseStudent",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "assigning_date",
                schema: "reporting",
                table: "CourseStudent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_currently_assigned",
                schema: "reporting",
                table: "CourseStudent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_course_student",
                schema: "reporting",
                table: "CourseStudent",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_course_student_course_id_student_id",
                schema: "reporting",
                table: "CourseStudent",
                columns: new[] { "course_id", "student_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_course_student",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.DropIndex(
                name: "ix_course_student_course_id_student_id",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.DropColumn(
                name: "assigning_date",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.DropColumn(
                name: "is_currently_assigned",
                schema: "reporting",
                table: "CourseStudent");

            migrationBuilder.AddPrimaryKey(
                name: "pk_course_student",
                schema: "reporting",
                table: "CourseStudent",
                columns: new[] { "course_id", "student_id" });
        }
    }
}
