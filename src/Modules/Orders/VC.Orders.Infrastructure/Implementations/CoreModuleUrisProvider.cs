using Microsoft.Extensions.DependencyInjection;
using VC.Orders.Application;

namespace VC.Orders.Infrastructure.Implementations;

internal class CoreModuleUrisProvider : ICoreModuleUrisProvider
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CoreModuleUrisProvider(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public string GetEmployeeById(Guid id)
    {
        var scope = _scopeFactory.CreateScope();

        

        throw new NotImplementedException();
    }

    public string GetServiceById(Guid id)
    {
        throw new NotImplementedException();
    }
}