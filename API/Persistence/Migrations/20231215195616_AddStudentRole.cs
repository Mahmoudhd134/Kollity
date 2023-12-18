using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
USE [my_college]
GO
INSERT [dbo].[Roles] ([id], [name], [normalized_name], [concurrency_stamp]) VALUES (N'4e07f76d-0de6-4c58-98f7-0a46c153fff5', N'Student', N'STUDENT', NULL)
GO
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
