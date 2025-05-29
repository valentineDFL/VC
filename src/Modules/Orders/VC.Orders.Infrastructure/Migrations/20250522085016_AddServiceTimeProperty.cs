using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTimeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceTime",
                schema: "orders",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTime",
                schema: "orders",
                table: "Orders");
        }
    }
}
