using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VC.Tenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTenantsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantDayWorkSchedule",
                schema: "tenants",
                columns: table => new
                {
                    TenantWorkScheduleTenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
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
        }
    }
}
