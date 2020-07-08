using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWiki.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WikiPages",
                columns: table => new
                {
                    WikiPageId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiPages", x => x.WikiPageId);
                });

            migrationBuilder.CreateTable(
                name: "WikiContents",
                columns: table => new
                {
                    WikiContentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    ChangeDescription = table.Column<string>(nullable: true),
                    Markdown = table.Column<string>(nullable: true),
                    WikiPageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiContents", x => x.WikiContentId);
                    table.ForeignKey(
                        name: "FK_WikiContents_WikiPages_WikiPageId",
                        column: x => x.WikiPageId,
                        principalTable: "WikiPages",
                        principalColumn: "WikiPageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WikiContents_WikiPageId",
                table: "WikiContents",
                column: "WikiPageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WikiContents");

            migrationBuilder.DropTable(
                name: "WikiPages");
        }
    }
}
