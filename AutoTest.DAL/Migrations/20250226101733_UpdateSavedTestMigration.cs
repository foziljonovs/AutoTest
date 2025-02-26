using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavedTestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "SavedTests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "test",
                table: "SavedTests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
