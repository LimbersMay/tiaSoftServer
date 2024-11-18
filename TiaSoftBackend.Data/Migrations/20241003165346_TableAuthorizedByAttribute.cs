using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableAuthorizedByAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentAuthorizedByUserId",
                table: "Tables",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_PaymentAuthorizedByUserId",
                table: "Tables",
                column: "PaymentAuthorizedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_AspNetUsers_PaymentAuthorizedByUserId",
                table: "Tables",
                column: "PaymentAuthorizedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_AspNetUsers_PaymentAuthorizedByUserId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_PaymentAuthorizedByUserId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PaymentAuthorizedByUserId",
                table: "Tables");
        }
    }
}
