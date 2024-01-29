using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditForeignKeyConstrainsInCourseTableDoctorIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_asp_net_users_doctor_id",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "fk_course_asp_net_users_doctor_id",
                table: "Course",
                column: "doctor_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_asp_net_users_doctor_id",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "fk_course_asp_net_users_doctor_id",
                table: "Course",
                column: "doctor_id",
                principalTable: "User",
                principalColumn: "id");
        }
    }
}
