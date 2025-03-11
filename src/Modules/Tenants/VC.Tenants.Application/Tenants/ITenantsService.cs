using FluentResults;
using VC.Tenants.Application.Tenants.Models;

namespace VC.Tenants.Application.Tenants;

public interface ITenantsService
{
    public Task<Result> CreateAsync(CreateTenantParams @params);
}