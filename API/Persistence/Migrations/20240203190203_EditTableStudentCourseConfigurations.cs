using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditTableStudentCourseConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_course_user_student_id",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "ix_student_course_student_id",
                table: "StudentCourse");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_student_id_course_id",
                table: "StudentCourse",
                columns: new[] { "student_id", "course_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_student_course_user_student_id",
                table: "StudentCourse",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_course_user_student_id",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "ix_student_course_student_id_course_id",
                table: "StudentCourse");

            migrationBuilder.CreateIndex(
                name: "ix_student_course_student_id",
                table: "StudentCourse",
                column: "student_id");

            migrationBuilder.AddForeignKey(
                name: "fk_student_course_user_student_id",
                table: "StudentCourse",
                column: "student_id",
                principalTable: "User",
                principalColumn: "id");
        }
    }
}
