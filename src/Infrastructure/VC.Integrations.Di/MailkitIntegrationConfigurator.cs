using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.MailkitIntegration;

namespace VC.Integrations.Di;

internal static class MailkitIntegrationConfigurator
{
    public static void ConfigureMailkitIntergration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSenderInfo>(configuration.GetSection(nameof(MailSenderInfo)));
        services.AddSingleton<ISendMailService, MailService>();
    }
}