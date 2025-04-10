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

            var config = new TenantConfiguration
            {
                About = "Тестовые данные компании",
                Currency = "USD",
                Language = "RU",
                TimeZoneId = "UTC"
            };

            var contactInfo = new ContactInfo
            {
                Address = "Ул. Пушкина Дом Колотушкина",
                Email = "testMail@Gmail.com",
                Phone = "+123456789"
            };

            var workSchedule = new TenantWorkSchedule()
            {
                DaysSchedule = new List<TenantDayWorkSchedule>
                {
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Sunday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Monday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Tuesday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Wednesday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Thursday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Friday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                    new TenantDayWorkSchedule()
                    {
                        Day = DayOfWeek.Saturday,
                        StartWork = DateTime.UtcNow,
                        EndWork = DateTime.UtcNow,
                    },
                }
            };

            var tenant = new Tenant
            {
                Id = Guid.CreateVersion7(),
                Name = "AdminTestTenant",
                Slug = SeedingDataBaseKeys.SeedTenantSlug,
                Config = config,
                Status = TenantStatus.Active,
                ContactInfo = contactInfo,
                WorkWeekSchedule = workSchedule
            };

            context.Set<Tenant>().Add(tenant);

            context.SaveChanges();
        });
    }
}