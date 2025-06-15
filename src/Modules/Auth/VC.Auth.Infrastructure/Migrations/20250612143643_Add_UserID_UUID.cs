using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserID_UUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "auth",
                table: "Users",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "auth",
                table: "Users");
        }
    }
}
