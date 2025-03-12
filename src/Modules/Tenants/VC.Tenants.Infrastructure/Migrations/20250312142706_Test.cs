using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                schema: "tenants",
                table: "TenantDayWorkSchedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndWork",
                schema: "tenants",
                table: "TenantDayWorkSchedule",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartWork",
                schema: "tenants",
                table: "TenantDayWorkSchedule",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                schema: "tenants",
                table: "TenantDayWorkSchedule");

            migrationBuilder.DropColumn(
                name: "EndWork",
                schema: "tenants",
                table: "TenantDayWorkSchedule");

            migrationBuilder.DropColumn(
                name: "StartWork",
                schema: "tenants",
                table: "TenantDayWorkSchedule");
        }
    }
}
