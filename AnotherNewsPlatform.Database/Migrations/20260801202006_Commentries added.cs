using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnotherNewsPlatform.Database.Migrations
{
    /// <inheritdoc />
    public partial class Commentriesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commentaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId1 = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commentaries_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commentaries_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_ArticleId",
                table: "Commentaries",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_UserId1",
                table: "Commentaries",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaries");
        }
    }
}
