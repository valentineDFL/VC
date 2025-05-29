using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatusesConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "orders",
                table: "Payments",
                newName: "State");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsStatuses_PaymentId",
                schema: "orders",
                table: "PaymentsStatuses",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersStatuses_OrderId",
                schema: "orders",
                table: "OrdersStatuses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersStatuses_Orders_OrderId",
                schema: "orders",
                table: "OrdersStatuses",
                column: "OrderId",
                principalSchema: "orders",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsStatuses_Payments_PaymentId",
                schema: "orders",
                table: "PaymentsStatuses",
                column: "PaymentId",
                principalSchema: "orders",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersStatuses_Orders_OrderId",
                schema: "orders",
                table: "OrdersStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsStatuses_Payments_PaymentId",
                schema: "orders",
                table: "PaymentsStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PaymentsStatuses_PaymentId",
                schema: "orders",
                table: "PaymentsStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrdersStatuses_OrderId",
                schema: "orders",
                table: "OrdersStatuses");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "orders",
                table: "Payments",
                newName: "Status");
        }
    }
}