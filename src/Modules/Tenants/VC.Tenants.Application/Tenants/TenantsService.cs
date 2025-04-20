using FluentResults;
using VC.Tenants.Application.Models.Create;
using VC.Tenants.Application.Models.Update;
using VC.Tenants.Entities;
using VC.Tenants.Repositories;
using VC.Tenants.UnitOfWork;
using VC.Utilities.Resolvers;

namespace VC.Tenants.Application.Tenants;

internal class TenantsService : ITenantsService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IDbSaver _dbSaver;
    private readonly ITenantResolver _tenantResolver;

    public TenantsService(ITenantRepository tenantRepository, IDbSaver dbSaver, ITenantResolver tenantResolver)
    {
        _tenantRepository = tenantRepository;
        _dbSaver = dbSaver;
        _tenantResolver = tenantResolver;
    }

    public async Task<Result> CreateAsync(CreateTenantParams @params)
    {
        var tenant = @params.ToEntity(Guid.CreateVersion7(), DateTime.UtcNow.AddMinutes(ContactInfo.LinkMinuteValidTime));

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
        var tenantId = _tenantResolver.Resolve();

        if (tenantId == Guid.Empty)
            return Result.Fail("Tenant Not Found");

        var tenant = @params.ToEntity(tenantId);

        _tenantRepository.Update(tenant);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result> VerifyEmailAsync()
    {
        var tenant = await _tenantRepository.GetAsync();

        if (tenant is null)
            return Result.Fail("Tenant Not Found");

        else if (tenant.ContactInfo.IsVerify)
            return Result.Fail("Tenant has already been verified");

        else if (tenant.ContactInfo.ConfirmationTimeExpired)
            return Result.Fail("Link has expired");

        tenant.VerifyEmail();

        _tenantRepository.Update(tenant);

        await _dbSaver.SaveAsync();

        return Result.Ok();
    }
}