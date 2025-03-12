using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalTenantUpdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tenants_Name",
                schema: "tenants",
                table: "Tenants");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tenants_Slug",
                schema: "tenants",
                table: "Tenants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tenants_Name",
                schema: "tenants",
                table: "Tenants",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tenants_Slug",
                schema: "tenants",
                table: "Tenants",
                column: "Slug");
        }
    }
}
