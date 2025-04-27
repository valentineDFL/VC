namespace VC.Tenants.Application;

internal class ByNameSlugGenerator : ISlugGenerator
{
    public string GenerateSlug(string name)
    {
        return $"https//{name}";
    }
}