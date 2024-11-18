using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class BillStatusRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillStatuses_BillStatusId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "BillStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillStatusId",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillStatusId",
                table: "Bills",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillStatuses",
                columns: table => new
                {
                    BillStatusId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStatuses", x => x.BillStatusId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills",
                column: "BillStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillStatuses_BillStatusId",
                table: "Bills",
                column: "BillStatusId",
                principalTable: "BillStatuses",
                principalColumn: "BillStatusId");
        }
    }
}
