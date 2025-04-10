using Microsoft.Extensions.DependencyInjection;
using VC.Utilities.Resolvers;
using VC.Utilities.MailSend;
using Microsoft.Extensions.Configuration;

namespace VC.Utilities;

public static class UtilitiesConfigurator
{
    public static void ConfigureUtilities(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITenantResolver, HttpContextTenantResolver>();
        services.Configure<MailSender>(configuration.GetSection(nameof(MailSender)));
        services.AddSingleton<IMailSenderService, MailService>();
    }
}