using FluentResults;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;
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
        var tenant = @params.ToEntity();

        await _tenantRepository.AddAsync(tenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
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

        var tenant = @params.ToEntity(existingTenant.Id);

        _tenantRepository.Update(existingTenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result> VerifyEmailAsync()
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant is null)
            return Result.Fail("Tenant Not Found");

        if (tenant.ContactInfo.ConfirmationTimeExpired)
            return Result.Fail("Link has expired");

        var oldContactInfo = tenant.ContactInfo;
        var updatedTenantContactInfo = ContactInfo.Create
            (
                oldContactInfo.Email,
                oldContactInfo.Phone, 
                oldContactInfo.Address, 
                true, 
                oldContactInfo.ConfirmationTime
            );

        var updatedTenant = Tenant.Create(tenant.Id, tenant.Name, tenant.Slug, tenant.Config, tenant.Status, tenant.ContactInfo, tenant.WorkWeekSchedule);

        _tenantRepository.Update(tenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }
}