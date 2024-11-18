using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class DailyOrderCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyOrderCounters",
                columns: table => new
                {
                    DailyOrderCounterId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOrderCounters", x => x.DailyOrderCounterId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyOrderCounters");
        }
    }
}
