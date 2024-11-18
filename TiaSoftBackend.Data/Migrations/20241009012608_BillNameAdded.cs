using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiaSoftBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class BillNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bills",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bills");
        }
    }
}
