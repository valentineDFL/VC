namespace VC.Tenants.Application;

internal interface ISlugGenerator
{
    public string GenerateSlug(string name);
}