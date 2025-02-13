using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_change",
                table: "Options",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_change",
                table: "Options");
        }
    }
}
