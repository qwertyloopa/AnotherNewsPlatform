using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnotherNewsPlatform.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedRatingToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Users");
        }
    }
}
