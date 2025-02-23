using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoTest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestSolutions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    test_id = table.Column<long>(type: "bigint", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    finished_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSolutions_Tests_test_id",
                        column: x => x.test_id,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSolutions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question_id = table.Column<long>(type: "bigint", nullable: false),
                    test_solution_id = table.Column<long>(type: "bigint", nullable: false),
                    selected_option_id = table.Column<long>(type: "bigint", nullable: true),
                    custom_answer = table.Column<string>(type: "text", nullable: true),
                    is_correct = table.Column<bool>(type: "boolean", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionSolutions_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSolutions_TestSolutions_test_solution_id",
                        column: x => x.test_solution_id,
                        principalTable: "TestSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTestSolutions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    test_solution_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTestSolutions_TestSolutions_test_solution_id",
                        column: x => x.test_solution_id,
                        principalTable: "TestSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTestSolutions_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSolutions_question_id",
                table: "QuestionSolutions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSolutions_test_solution_id",
                table: "QuestionSolutions",
                column: "test_solution_id");

            migrationBuilder.CreateIndex(
                name: "IX_TestSolutions_test_id",
                table: "TestSolutions",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestSolutions_test_solution_id",
                table: "UserTestSolutions",
                column: "test_solution_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestSolutions_user_id",
                table: "UserTestSolutions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionSolutions");

            migrationBuilder.DropTable(
                name: "UserTestSolutions");

            migrationBuilder.DropTable(
                name: "TestSolutions");
        }
    }
}
