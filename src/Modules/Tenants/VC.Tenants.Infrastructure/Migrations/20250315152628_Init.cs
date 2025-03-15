using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tenants");

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Config_About = table.Column<string>(type: "text", nullable: false),
                    Config_Currency = table.Column<string>(type: "text", nullable: false),
                    Config_Language = table.Column<string>(type: "text", nullable: false),
                    Config_TimeZoneId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "text", nullable: true),
                    ContactInfo_Phone = table.Column<string>(type: "text", nullable: true),
                    ContactInfo_Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantDayWorkSchedule",
                schema: "tenants",
                columns: table => new
                {
                    TenantWorkScheduleTenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartWork = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndWork = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantDayWorkSchedule", x => new { x.TenantWorkScheduleTenantId, x.Id });
                    table.ForeignKey(
                        name: "FK_TenantDayWorkSchedule_Tenants_TenantWorkScheduleTenantId",
                        column: x => x.TenantWorkScheduleTenantId,
                        principalSchema: "tenants",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantDayWorkSchedule",
                schema: "tenants");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "tenants");
        }
    }
}
