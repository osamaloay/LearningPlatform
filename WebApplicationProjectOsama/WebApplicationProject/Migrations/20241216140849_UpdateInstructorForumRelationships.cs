using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstructorForumRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "disscussion_forum",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstructorDisscussions",
                columns: table => new
                {
                    ForumId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorDisscussions", x => new { x.ForumId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_InstructorDisscussions_disscussion_forum_ForumId",
                        column: x => x.ForumId,
                        principalTable: "disscussion_forum",
                        principalColumn: "forum_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorDisscussions_instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "instructor",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_disscussion_forum_InstructorId",
                table: "disscussion_forum",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorDisscussions_InstructorId",
                table: "InstructorDisscussions",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_disscussion_forum_instructor_InstructorId",
                table: "disscussion_forum",
                column: "InstructorId",
                principalTable: "instructor",
                principalColumn: "instructor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_disscussion_forum_instructor_InstructorId",
                table: "disscussion_forum");

            migrationBuilder.DropTable(
                name: "InstructorDisscussions");

            migrationBuilder.DropIndex(
                name: "IX_disscussion_forum_InstructorId",
                table: "disscussion_forum");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "disscussion_forum");
        }
    }
}
