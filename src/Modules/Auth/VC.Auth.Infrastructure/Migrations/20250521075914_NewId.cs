using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "auth",
                table: "Users",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "auth",
                table: "Users",
                newName: "TenantId");
        }
    }
}
