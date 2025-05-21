using Mapster;
using Microsoft.AspNetCore.CookiePolicy;
using Scalar.AspNetCore;
using Serilog;
using VC.Auth.Api.Middleware;
using VC.Host;
using VC.Auth.Di;
using VC.Integrations.Di;
using VC.Services.Di;
using VC.Tenants.Di;
using VC.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(VC.Tenants.Api.Entry).Assembly)
    .AddApplicationPart(typeof(VC.Services.Api.Entry).Assembly)
    .AddApplicationPart(typeof(VC.Auth.Api.Entry).Assembly);
builder.Services.ConfigureTenantsModule(builder.Configuration);
builder.Services.ConfigureUtilities(builder.Configuration);
builder.Services.ConfigureIntegrationsModule(builder.Configuration);
builder.Services.ConfigureServicesModule(builder.Configuration);
builder.Services.ConfigureAuthModule(builder.Configuration);

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

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.UseHttpLogging();

app.Run();