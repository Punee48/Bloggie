﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "BlogPostComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostComments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostLikes_BlogPostId",
                table: "BlogPostLikes",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComments_BlogPostId",
                table: "BlogPostComments",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes");

            migrationBuilder.DropTable(
                name: "BlogPostComments");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostLikes_BlogPostId",
                table: "BlogPostLikes");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }
    }
}
