using Asp.Versioning;
using OpenTelemetry.Metrics;
using Scalar.AspNetCore;
using Serilog;
using VC.Tenants.Api.Controller;
using VC.Tenants.Di;
using VC.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(typeof(TenantsController).Assembly);
builder.Services.ConfigureTenantsModule(builder.Configuration);
builder.Services.ConfigureUtilities();

builder.Services.AddHttpLogging();
builder.Services.AddHealthChecks();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddOpenApi("home", opts =>
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
        }
    );
});

builder.Services.AddOpenTelemetry()
    .WithMetrics(b =>
    {
        b.AddMeter("VC", "Npgsql");
        b.AddProcessInstrumentation()
            .AddRuntimeInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
        b.AddPrometheusExporter();
    });

builder.Services.AddApiVersioning(options =>
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

VC.Tenants.Di.ModuleConfiguration.Configure(builder.Services, builder.Configuration);
VC.Bookings.Di.ModuleConfiguration.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();
app.MapHealthChecks("/health");
app.MapOpenApi();
app.MapScalarApiReference(opts =>
{
    opts.Theme = ScalarTheme.BluePlanet;
    opts.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    opts.ShowSidebar = true;
});

app.MapControllers();

app.UseHttpLogging();

app.Run();