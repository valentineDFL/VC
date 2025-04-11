using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactInfoOwn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ContactInfo_ConfirmationTime",
                schema: "tenants",
                table: "Tenants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ContactInfo_IsVerify",
                schema: "tenants",
                table: "Tenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactInfo_ConfirmationTime",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ContactInfo_IsVerify",
                schema: "tenants",
                table: "Tenants");
        }
    }
}
