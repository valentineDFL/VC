using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VC.Core.Employees;

#nullable disable

namespace VC.Core.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalSchema: "core",
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Specialisation = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    BasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    BaseDuration = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredResources = table.Column<Guid[]>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Items = table.Column<IReadOnlyCollection<WorkScheduleItem>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAssignments",
                schema: "core",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAssignments", x => new { x.ServiceId, x.Id });
                    table.ForeignKey(
                        name: "FK_EmployeeAssignments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "core",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHourException",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDayOff = table.Column<bool>(type: "boolean", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkScheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHourException", x => x.Id);
                    table.CheckConstraint("CK_WorkingHourException_DayOffTimes", "\"IsDayOff\" = false AND \"StartTime\" IS NOT NULL AND \"EndTime\" IS NOT NULL OR \r\n              \"IsDayOff\" = true AND \"StartTime\" IS NULL AND \"EndTime\" IS NULL");
                    table.CheckConstraint("CK_WorkingHourException_TimeRange", "\"IsDayOff\" = true OR \"StartTime\" < \"EndTime\"");
                    table.ForeignKey(
                        name: "FK_WorkingHourException_WorkSchedules_WorkScheduleId",
                        column: x => x.WorkScheduleId,
                        principalSchema: "core",
                        principalTable: "WorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                schema: "core",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAssignments_EmployeeId",
                schema: "core",
                table: "EmployeeAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                schema: "core",
                table: "Services",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_IsActive",
                schema: "core",
                table: "Services",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Services_TenantId",
                schema: "core",
                table: "Services",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourException_Date",
                schema: "core",
                table: "WorkingHourException",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourException_EmployeeId",
                schema: "core",
                table: "WorkingHourException",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourException_EmployeeId_Date",
                schema: "core",
                table: "WorkingHourException",
                columns: new[] { "EmployeeId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourException_TenantId",
                schema: "core",
                table: "WorkingHourException",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourException_WorkScheduleId",
                schema: "core",
                table: "WorkingHourException",
                column: "WorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_EmployeeId",
                schema: "core",
                table: "WorkSchedules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_TenantId",
                schema: "core",
                table: "WorkSchedules",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "core");

            migrationBuilder.DropTable(
                name: "EmployeeAssignments",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "core");

            migrationBuilder.DropTable(
                name: "WorkingHourException",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "core");

            migrationBuilder.DropTable(
                name: "WorkSchedules",
                schema: "core");
        }
    }
}
