namespace VC.Tenants.Application;

public interface ISlugGenerator
{
    public string GenerateSlug(string name);
}