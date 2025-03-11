using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTenantRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_ContactInfo_ContactInfoId",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_ContactInfoId",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "tenants",
                table: "ContactInfo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_TenantId",
                schema: "tenants",
                table: "ContactInfo",
                column: "TenantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfo_Tenants_TenantId",
                schema: "tenants",
                table: "ContactInfo",
                column: "TenantId",
                principalSchema: "tenants",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Tenants_TenantId",
                schema: "tenants",
                table: "ContactInfo");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfo_TenantId",
                schema: "tenants",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "tenants",
                table: "ContactInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ContactInfoId",
                schema: "tenants",
                table: "Tenants",
                column: "ContactInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_ContactInfo_ContactInfoId",
                schema: "tenants",
                table: "Tenants",
                column: "ContactInfoId",
                principalSchema: "tenants",
                principalTable: "ContactInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
