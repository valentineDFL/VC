using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VC.Tenants.Entities;
using VC.Utilities.Resolvers;

namespace VC.Tenants.Infrastructure.Persistence;

public class TenantsDbContext : DbContext
{
    private readonly ITenantResolver _tenantResolver;
    private readonly TenantsModuleSettings _tenantModuleOptions;

    public TenantsDbContext(
        DbContextOptions<TenantsDbContext> options, 
        ITenantResolver tenantResolver,
        IOptions<TenantsModuleSettings> tenantModuleSettings) : base(options)
    {
        _tenantResolver = tenantResolver;
        _tenantModuleOptions = tenantModuleSettings.Value;
        Database.EnsureCreated();
    }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tenants");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantsDbContext).Assembly);

        modelBuilder.Entity<Tenant>()
            .HasQueryFilter(t => t.Id == _tenantResolver.Resolve());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((context, flag) =>
        {
            if (!_tenantModuleOptions.SeedingSettings.IsEnabled)
                return;

            var isTenantExists = context.Set<Tenant>()
                .IgnoreQueryFilters()
                .Any(t => t.Slug == SeedingDataBaseKeys.SeedTenantSlug);

            if (!isTenantExists)
                return;

            Tenant tenant = CreateSeedingTenant();

            context.Set<Tenant>().Add(tenant);

            context.SaveChanges();
        });
    }

    private Tenant CreateSeedingTenant()
    {
        var config = TenantConfiguration.Create
            (
                "Тестовые данные компании",
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

        var workScheduleDays = new List<TenantDayWorkSchedule>
            {
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Sunday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                ),
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Monday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                ),
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Tuesday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                ),
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Wednesday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                ),
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Thursday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                ),
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Friday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                ),
                TenantDayWorkSchedule.Create
                (
                    DayOfWeek.Saturday,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                )
            };

        var workSchedule = TenantWorkSchedule.Create
        (
            workScheduleDays
        );

        return Tenant.Create
        (
            Guid.CreateVersion7(),
            "AdminTestTenant",
            SeedingDataBaseKeys.SeedTenantSlug,
            config,
            TenantStatus.Active,
            contactInfo,
            workSchedule
        );
    }
}