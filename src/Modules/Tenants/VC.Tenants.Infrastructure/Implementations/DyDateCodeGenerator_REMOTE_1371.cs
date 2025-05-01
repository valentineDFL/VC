using VC.Tenants.Application;

namespace VC.Tenants.Infrastructure.Implementations;

internal class DyDateCodeGenerator : IEmailVerifyCodeGenerator
{
    public string GenerateCode()
    {
        int hashCode = DateTime.UtcNow.GetHashCode();
        return hashCode > 0 ? hashCode.ToString() : (-hashCode).ToString();
    }
}