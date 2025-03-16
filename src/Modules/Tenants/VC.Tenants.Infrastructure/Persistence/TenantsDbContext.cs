using Microsoft.EntityFrameworkCore;
using VC.Tenants.Entities;
using VC.Utilities;
using VC.Utilities.Resolvers;

namespace VC.Tenants.Infrastructure.Persistence;

public class TenantsDbContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }

    private readonly ITenantResolver _tenantResolver;

    public TenantsDbContext(DbContextOptions<TenantsDbContext> options, ITenantResolver tenantResolver) : base(options)
    {
        Database.EnsureCreated();
        _tenantResolver = tenantResolver;
    }

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
            var findedTestTenant = context.Set<Tenant>()
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Slug == Utilities.SeedingDataBaseKeys.SeedTenantSlug);

            if (findedTestTenant != null)
                return;

            var config = new TenantConfiguration()
            {
                About = "Тестовые данные компании",
                Currency = "USD",
                Language = "RU",
                TimeZoneId = "UTC"
            };

            var contactInfo = new ContactInfo()
            {
                Address = "Ул. Пушкина Дом Колотушкина",
                Email = "testMail@Gmail.com",
                Phone = "+123456789"
            };

            var workSchedule = new TenantWorkSchedule()
            {
                DaysSchedule = new List<TenantDayWorkSchedule>()
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

            var tenant = new Tenant()
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