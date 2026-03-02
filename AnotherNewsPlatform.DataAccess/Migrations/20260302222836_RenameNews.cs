using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnotherNewsPlatform.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "News",
                newName: "Article");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Article",
                table: "News",
                newName: "Title");
        }
    }
}
