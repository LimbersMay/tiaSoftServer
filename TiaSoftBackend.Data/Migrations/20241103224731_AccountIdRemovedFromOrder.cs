using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccountIdRemovedFromOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Orders",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);
        }
    }
}
