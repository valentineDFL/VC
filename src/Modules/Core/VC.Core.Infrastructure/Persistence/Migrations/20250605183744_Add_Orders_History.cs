using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Core.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Orders_History : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersHistory",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeesIds = table.Column<Guid[]>(type: "uuid[]", nullable: false),
                    ServiceTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_ServiceTime",
                schema: "core",
                table: "OrdersHistory",
                column: "ServiceTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersHistory",
                schema: "core");
        }
    }
}
