using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kollity.Feedback.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "feedback",
                table: "FeedbackQuestion",
                columns: new[] { "id", "category", "is_mcq", "question" },
                values: new object[,]
                {
                    { new Guid("24177208-8ff8-4a84-a593-c5dbed9aadd8"), 1, false, "This is question three for course category" },
                    { new Guid("333f54dd-7804-4a1a-928d-a8206eb95286"), 2, true, "This is question two for doctor category" },
                    { new Guid("3455fa29-f38a-499e-ad13-68052d32dedb"), 2, false, "This is question three for doctor category" },
                    { new Guid("43355478-d19d-45aa-abf3-8c576844a299"), 1, true, "This is question two for course category" },
                    { new Guid("573472f1-76ae-43a4-887e-8c078b2e9cad"), 1, true, "This is question one for course category" },
                    { new Guid("a9124653-d42d-4b77-8dd3-0201a116220f"), 3, true, "This is question one for exam category" },
                    { new Guid("af166444-2b76-49f9-9111-68d119477d3a"), 3, true, "This is question two for exam category" },
                    { new Guid("b08b7bbc-c6b2-4299-b76d-9426d19acb90"), 3, false, "This is question three for exam category" },
                    { new Guid("c0399a84-c7b3-436b-a5f3-0e3ff4d91ead"), 2, true, "This is question one for doctor category" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("24177208-8ff8-4a84-a593-c5dbed9aadd8"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("333f54dd-7804-4a1a-928d-a8206eb95286"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("3455fa29-f38a-499e-ad13-68052d32dedb"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("43355478-d19d-45aa-abf3-8c576844a299"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("573472f1-76ae-43a4-887e-8c078b2e9cad"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("a9124653-d42d-4b77-8dd3-0201a116220f"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("af166444-2b76-49f9-9111-68d119477d3a"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("b08b7bbc-c6b2-4299-b76d-9426d19acb90"));

            migrationBuilder.DeleteData(
                schema: "feedback",
                table: "FeedbackQuestion",
                keyColumn: "id",
                keyValue: new Guid("c0399a84-c7b3-436b-a5f3-0e3ff4d91ead"));
        }
    }
}
