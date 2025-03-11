using FluentResults;
using VC.Tenants.Application.Tenants.Models;
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

    public Task<Result> CreateAsync(CreateTenantParams @params)
    {
        throw new NotImplementedException();
    }
}