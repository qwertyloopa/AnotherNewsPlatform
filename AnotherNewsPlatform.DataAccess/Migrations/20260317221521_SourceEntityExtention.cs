using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnotherNewsPlatform.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SourceEntityExtention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_NewsPublishers_newsPublisherId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsPublishers_SourceId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsPublishers",
                table: "NewsPublishers");

            migrationBuilder.RenameTable(
                name: "NewsPublishers",
                newName: "Sources");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Sources",
                newName: "DomainUrl");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sources",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sources",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCrawled",
                table: "Sources",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RssUrl",
                table: "Sources",
                type: "character varying(550)",
                maxLength: 550,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sources",
                table: "Sources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Sources_newsPublisherId",
                table: "Authors",
                column: "newsPublisherId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Sources_SourceId",
                table: "News",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Sources_newsPublisherId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Sources_SourceId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sources",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "LastCrawled",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "RssUrl",
                table: "Sources");

            migrationBuilder.RenameTable(
                name: "Sources",
                newName: "NewsPublishers");

            migrationBuilder.RenameColumn(
                name: "DomainUrl",
                table: "NewsPublishers",
                newName: "Url");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsPublishers",
                table: "NewsPublishers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_NewsPublishers_newsPublisherId",
                table: "Authors",
                column: "newsPublisherId",
                principalTable: "NewsPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsPublishers_SourceId",
                table: "News",
                column: "SourceId",
                principalTable: "NewsPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
