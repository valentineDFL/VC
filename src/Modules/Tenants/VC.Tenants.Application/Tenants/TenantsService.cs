using FluentResults;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;
using VC.Tenants.Infrastructure;
using VC.Tenants.Repositories;

namespace VC.Tenants.Application.Tenants;

internal class TenantsService : ITenantsService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IDbSaver _dbSaver;

    public TenantsService(ITenantRepository tenantRepository, IDbSaver dbSaver)
    {
        _tenantRepository = tenantRepository;
        _dbSaver = dbSaver;
    }

    public async Task<Result> CreateAsync(CreateTenantParams @params)
    {
        var tenant = BuildTenant(@params);

        await _tenantRepository.AddAsync(tenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    private Tenant BuildTenant(CreateTenantParams @params)
    {
        var configuration = BuildConfiguration(@params.TenantConfig);

        var contactInfo = BuildContactInfo(@params.Contact);

        var workSchedule = BuildTenantWorkSchedule(@params.WorkSchedule);

        return new Tenant()
        {
            Id = Guid.NewGuid(),
            Name = @params.Name,
            Slug = @params.Slug,
            Config = configuration,
            Status = @params.TenantStatus,
            ContactInfo = contactInfo,
            WorkWeekSchedule = workSchedule
        };
    }

    private Tenant BuildTenant(UpdateTenantParams @params)
    {
        var configuration = BuildConfiguration(@params.TenantConfig);

        var contactInfo = BuildContactInfo(@params.Contact);

        var workSchedule = BuildTenantWorkSchedule(@params.WorkSchedule);

        return new Tenant()
        {
            Id = @params.Id,
            Name = @params.Name,
            Slug = @params.Slug,
            Config = configuration,
            Status = @params.TenantStatus,
            ContactInfo = contactInfo,
            WorkWeekSchedule = workSchedule
        };
    }

    private TenantConfiguration BuildConfiguration(TenantConfigurationDto dto)
    {
        return new TenantConfiguration()
        {
            About = dto.About,
            Currency = dto.Currency,
            Language = dto.Language,
            TimeZoneId = dto.TimeZoneId,
        };
    }

    private ContactInfo BuildContactInfo(ContactDto dto)
    {
        return new ContactInfo()
        {
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
        };
    }

    private TenantWorkSchedule BuildTenantWorkSchedule(TenantWeekWorkSheduleDto dto)
    {
        var daysSchedule = new List<TenantDayWorkSchedule>();

        foreach (var day in dto.WorkDays)
        {
            daysSchedule.Add(new TenantDayWorkSchedule()
            {
                Day = day.Day,
                StartWork = day.StartWork.ToUniversalTime(),
                EndWork = day.EndWork.ToUniversalTime()
            });
        }

        return new TenantWorkSchedule() { DaysSchedule = daysSchedule };
    }

    public async Task<Result> DeleteAsync()
    {
        var existingTenant = await _tenantRepository.GetAsync();

        if (existingTenant is null)
            return Result.Fail("Tenant Not found");

        _tenantRepository.Remove(existingTenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result<Tenant>> GetAsync()
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant is null)
            return Result.Fail("Tenant Not Found");

        return Result.Ok(tenant);
    }

    public async Task<Result> UpdateAsync(UpdateTenantParams @params)
    {
        var existingTenant = await _tenantRepository.GetAsync();

        if (existingTenant is null)
            return Result.Fail("Tenant not found");

        var tenant = BuildTenant(@params);

        UpdateFindedTenant(tenant, existingTenant);

        _tenantRepository.Update(existingTenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    private void UpdateFindedTenant(Tenant from, Tenant to)
    {
        to.Name = from.Name;

        to.Slug = from.Slug;

        to.Config = from.Config;

        to.Status = from.Status;

        to.ContactInfo = from.ContactInfo;

        to.WorkWeekSchedule = from.WorkWeekSchedule;
    }
}