using Mapster;
using Scalar.AspNetCore;
using Serilog;
using VC.Host;
using VC.Integrations.Di;
using VC.Services.Di;
using VC.Tenants.Di;
using VC.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(VC.Tenants.Api.Entry).Assembly)
    .AddApplicationPart(typeof(VC.Services.Api.Entry).Assembly);
builder.Services.ConfigureTenantsModule(builder.Configuration);
builder.Services.ConfigureUtilities(builder.Configuration);
builder.Services.ConfigureIntegrationsModule(builder.Configuration);
builder.Services.ConfigureServicesModule(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpLogging();
builder.Services.AddHealthChecks();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.ConfigureHost();
builder.Services.AddMapster();

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

app.UseHttpLogging();
app.MapControllers();

app.Run();