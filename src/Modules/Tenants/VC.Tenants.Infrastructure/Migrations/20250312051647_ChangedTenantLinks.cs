using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTenantLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfo",
                schema: "tenants");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "TimeZoneId",
                schema: "tenants",
                table: "Tenants",
                newName: "Config_TimeZoneId");

            migrationBuilder.AddColumn<string>(
                name: "Config_About",
                schema: "tenants",
                table: "Tenants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Config_Currency",
                schema: "tenants",
                table: "Tenants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Config_Language",
                schema: "tenants",
                table: "Tenants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_Address",
                schema: "tenants",
                table: "Tenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_Email",
                schema: "tenants",
                table: "Tenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_Phone",
                schema: "tenants",
                table: "Tenants",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Config_About",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Config_Currency",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Config_Language",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ContactInfo_Address",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ContactInfo_Email",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ContactInfo_Phone",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "Config_TimeZoneId",
                schema: "tenants",
                table: "Tenants",
                newName: "TimeZoneId");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                schema: "tenants",
                table: "Tenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                schema: "tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfo_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "tenants",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_TenantId",
                schema: "tenants",
                table: "ContactInfo",
                column: "TenantId",
                unique: true);
        }
    }
}
