using FluentResults;
using Microsoft.AspNetCore.Mvc;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Models;
using VC.Tenants.Repositories;
using VC.Tenants.UnitOfWork;

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
        Tenant tenant = BuildTenant(@params);

        await _tenantRepository.AddAsync(tenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    private Tenant BuildTenant(CreateTenantParams @params)
    {
        TenantConfiguration configuration = BuildConfiguration(@params.TenantConfig);

        ContactInfo contactInfo = BuildContactInfo(@params.Contact);

        TenantWorkSchedule workSchedule = BuildTenantWorkSchedule(@params.WorkSchedule);

        return new Tenant()
        {
            Id = Guid.NewGuid(),
            Name = @params.Name,
            Slug = @params.Slug,
            Config = configuration,
            Status = @params.TenantStatus,
            ContactInfo = contactInfo,
            WorkWeekSchedule = workSchedule
        }; ;
    }

    private Tenant BuildTenant(UpdateTenantParams @params)
    {
        TenantConfiguration configuration = BuildConfiguration(@params.TenantConfig);

        ContactInfo contactInfo = BuildContactInfo(@params.Contact);

        TenantWorkSchedule workSchedule = BuildTenantWorkSchedule(@params.WorkSchedule);

        return new Tenant()
        {
            Id = @params.Id,
            Name = @params.Name,
            Slug = @params.Slug,
            Config = configuration,
            Status = @params.TenantStatus,
            ContactInfo = contactInfo,
            WorkWeekSchedule = workSchedule
        }; ;
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
        List<TenantDayWorkSchedule> daysSchedule = new List<TenantDayWorkSchedule>();

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

    public async Task<Result> DeleteAsync(Guid tenantId)
    {
        var tenant = await _tenantRepository.GetByIdAsync(tenantId);

        if (tenant == null)
            return Result.Fail("Not found");

        _tenantRepository.Remove(tenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result<Tenant>> GetByIdAsync(Guid tenantId)
    {
        var tenant = await _tenantRepository.GetByIdAsync(tenantId);

        if (tenant == null)
            return Result.Fail("Tenant Not Found");

        return Result.Ok(tenant);
    }

    public async Task<Result> UpdateAsync(UpdateTenantParams @params)
    {
        var tenantWithTurnedId = await _tenantRepository.GetByIdAsync(@params.Id);

        if (tenantWithTurnedId == null)
            return Result.Fail("Tenant not found");

        Tenant tenant = BuildTenant(@params);

        UpdateFindedTenant(tenant, tenantWithTurnedId);

        _tenantRepository.Update(tenantWithTurnedId);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    private void UpdateFindedTenant(Tenant from, Tenant to)
    {
        // name and slug update
        to.Name = from.Name;
        to.Slug = from.Slug;

        // config update
        to.Config = from.Config;

        // status update
        to.Status = from.Status;

        // contact info update
        to.ContactInfo = from.ContactInfo;

        // tenantWorkSchedule update
        to.WorkWeekSchedule = from.WorkWeekSchedule;
    }
}