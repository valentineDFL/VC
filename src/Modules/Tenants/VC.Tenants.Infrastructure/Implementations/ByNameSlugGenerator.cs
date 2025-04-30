using VC.Tenants.Application;

namespace VC.Tenants.Infrastructure.Implementations;

internal class ByNameSlugGenerator : ISlugGenerator
{
    public string GenerateSlug(string name)
    {
        return $"https//{name}";
    }
}