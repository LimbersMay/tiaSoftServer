using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderProductStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderProductStatusId",
                table: "OrderProducts",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderProductStatuses",
                columns: table => new
                {
                    OrderProductStatusId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductStatuses", x => x.OrderProductStatusId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderProductStatusId",
                table: "OrderProducts",
                column: "OrderProductStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_OrderProductStatuses_OrderProductStatusId",
                table: "OrderProducts",
                column: "OrderProductStatusId",
                principalTable: "OrderProductStatuses",
                principalColumn: "OrderProductStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_OrderProductStatuses_OrderProductStatusId",
                table: "OrderProducts");

            migrationBuilder.DropTable(
                name: "OrderProductStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderProductStatusId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OrderProductStatusId",
                table: "OrderProducts");
        }
    }
}
