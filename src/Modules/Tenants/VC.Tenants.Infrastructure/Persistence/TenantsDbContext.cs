using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VC.Tenants.Entities;
using VC.Utilities;
using VC.Utilities.Resolvers;

namespace VC.Tenants.Infrastructure.Persistence;

public class TenantsDbContext : DbContext
{
    public const string SchemaName = "tenants";

    private readonly ITenantResolver _tenantResolver;
    private readonly TenantsModuleSettings _tenantModuleSettings;

    public TenantsDbContext(
        DbContextOptions<TenantsDbContext> options, 
        ITenantResolver tenantResolver,
        IOptions<TenantsModuleSettings> tenantModuleSettings) : base(options)
    {
        _tenantResolver = tenantResolver;
        _tenantModuleSettings = tenantModuleSettings.Value;
        Database.EnsureCreated();
    }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantsDbContext).Assembly);

        modelBuilder.Entity<Tenant>()
            .HasQueryFilter(t => t.Id == _tenantResolver.Resolve());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((context, flag) =>
        {
            if (!_tenantModuleSettings.SeedingSettings.IsEnabled)
                return;

            var isTenantExists = context.Set<Tenant>()
                .IgnoreQueryFilters()
                .Any(t => t.Slug == SeedingDataBaseKeys.SeedTenantSlug);

            if (isTenantExists)
                return;

            Tenant tenant = CreateSeedingTenant();

            context.Set<Tenant>().Add(tenant);

            context.SaveChanges();
        });
    }

    private Tenant CreateSeedingTenant()
    {
        var tenantId = TenantsIds.StaticTenantId;

        var config = TenantConfiguration.Create
            (
                "�������� ������ ��������",
                "USD",
                "RU",
                "UTC"
            );

        var address = Address.Create
        (
            "Ukraine",
            "Kiev",
            "Pushkina Street",
            456
        );

        var contactInfo = ContactInfo.Create
        (
            "testMail@Gmail.com",
            "+123456789",
            address,
            true,
            DateTime.UtcNow
        );

        var workSchedule = new List<DaySchedule>
        {
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Sunday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            ),
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Monday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            ),
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Tuesday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            ),
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Wednesday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            ),
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Thursday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            ),
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Friday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            ),
            DaySchedule.Create
            (
                Guid.CreateVersion7(),
                tenantId,
                DayOfWeek.Saturday,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8)
            )
        };

        return Tenant.Create
        (
            tenantId,
            "AdminTestTenant",
            SeedingDataBaseKeys.SeedTenantSlug,
            config,
            TenantStatus.Active,
            contactInfo,
            workSchedule
        );
    }
}