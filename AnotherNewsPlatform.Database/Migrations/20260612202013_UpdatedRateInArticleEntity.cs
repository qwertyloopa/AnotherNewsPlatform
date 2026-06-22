using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnotherNewsPlatform.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRateInArticleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Users");

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Articles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Articles");

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
