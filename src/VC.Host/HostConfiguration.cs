using Asp.Versioning;
using OpenTelemetry.Metrics;

namespace VC.Host;

internal static class HostConfiguration
{
    public static void ConfigureHost(this IServiceCollection services)
    {
        AddOpenApi(services);
        AddOpenTelemetry(services);
        AddApiVersioning(services);
    }

    private static void AddOpenApi(IServiceCollection services)
    {
        services.AddOpenApi("home", opts =>
        {
            opts.ShouldInclude = description => description.GroupName == "Home API";
            opts.AddDocumentTransformer((document, ctx, ctl) =>
            {
                document.Info = new()
                {
                    Version = "v1",
                    Title = "Универсальная платформа для управления услугами и онлайн-бронирования с поддержкой мультитенантности",
                    Description = """
                              <a href="http://localhost:5056/scalar/tenants">Управление арендаторами</a><br/>
                              <a href="http://localhost:5056/scalar/bookings">Управление бронированиями</a><br/>
                         
                              GitLab - https://gitlab.com/tech-power-partners/vclients/vc
                              """
                };

                return Task.CompletedTask;
            });
        });
    }

    private static void AddOpenTelemetry(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(b =>
            {
                b.AddMeter("VC", "Npgsql");

                b.AddProcessInstrumentation()
                 .AddRuntimeInstrumentation()
                 .AddAspNetCoreInstrumentation()
                 .AddHttpClientInstrumentation();

                b.AddPrometheusExporter();
            });
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}