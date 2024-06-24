﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wuchmiITHome.Migrations
{
    /// <inheritdoc />
    public partial class ArticleAddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Article",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Article");
        }
    }
}
