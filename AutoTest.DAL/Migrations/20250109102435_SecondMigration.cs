using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Tests_test_id",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_test_id",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "test_id",
                table: "Topics");

            migrationBuilder.CreateTable(
                name: "TestTopic",
                columns: table => new
                {
                    TestsId = table.Column<long>(type: "bigint", nullable: false),
                    TopicsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTopic", x => new { x.TestsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_TestTopic_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestTopic_TopicsId",
                table: "TestTopic",
                column: "TopicsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestTopic");

            migrationBuilder.AddColumn<long>(
                name: "test_id",
                table: "Topics",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_test_id",
                table: "Topics",
                column: "test_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Tests_test_id",
                table: "Topics",
                column: "test_id",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
