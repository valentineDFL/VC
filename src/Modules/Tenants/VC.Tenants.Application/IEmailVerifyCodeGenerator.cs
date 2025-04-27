namespace VC.Tenants.Application;

public interface IEmailVerifyCodeGenerator
{
    public string GenerateCode();
}