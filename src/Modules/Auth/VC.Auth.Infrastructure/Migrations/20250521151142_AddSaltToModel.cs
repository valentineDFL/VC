using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VC.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSaltToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                schema: "auth",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                schema: "auth",
                table: "Users");
        }
    }
}
