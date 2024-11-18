using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class BillEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "Orders",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillStatuses",
                columns: table => new
                {
                    BillStatusId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Status = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStatuses", x => x.BillStatusId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", maxLength: 36, nullable: false),
                    BillStatusId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    TableId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_BillStatuses_BillStatusId",
                        column: x => x.BillStatusId,
                        principalTable: "BillStatuses",
                        principalColumn: "BillStatusId");
                    table.ForeignKey(
                        name: "FK_Bills_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillId",
                table: "Orders",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills",
                column: "BillStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TableId",
                table: "Bills",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bills_BillId",
                table: "Orders",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bills_BillId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "BillStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BillId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Orders");
        }
    }
}
