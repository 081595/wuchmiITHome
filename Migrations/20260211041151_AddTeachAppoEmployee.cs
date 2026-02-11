using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wuchmiITHome.Migrations
{
    /// <inheritdoc />
    public partial class AddTeachAppoEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeachAppoEmployees",
                columns: table => new
                {
                    Yr = table.Column<int>(type: "INTEGER", nullable: false),
                    IdNo = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmplNo = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    ChName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    EnName = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    RefreshTokenExpired = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SeqNo = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachAppoEmployees", x => new { x.Yr, x.IdNo, x.Birthday });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachAppoEmployees");
        }
    }
}
