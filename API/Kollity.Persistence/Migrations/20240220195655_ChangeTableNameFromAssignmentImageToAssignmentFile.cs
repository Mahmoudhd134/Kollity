using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kollity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNameFromAssignmentImageToAssignmentFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentImage");

            migrationBuilder.CreateTable(
                name: "AssignmentFile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_file_assigment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "Assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_file_assignment_id",
                table: "AssignmentFile",
                column: "assignment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentFile");

            migrationBuilder.CreateTable(
                name: "AssignmentImage",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    image = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_image_assigment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "Assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_image_assignment_id",
                table: "AssignmentImage",
                column: "assignment_id");
        }
    }
}
