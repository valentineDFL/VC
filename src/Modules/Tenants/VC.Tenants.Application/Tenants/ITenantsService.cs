using FluentResults;
using VC.Tenants.Application.Tenants.Models;

namespace VC.Tenants.Application.Tenants;

public interface ITenantsService
{
    Task<Result> CreateAsync(CreateTenantDto dto);
}