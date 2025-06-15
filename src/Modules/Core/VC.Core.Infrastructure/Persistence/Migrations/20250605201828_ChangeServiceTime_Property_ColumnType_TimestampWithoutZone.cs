using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Core.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeServiceTime_Property_ColumnType_TimestampWithoutZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceTime",
                schema: "core",
                table: "OrdersHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceTime",
                schema: "core",
                table: "OrdersHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
