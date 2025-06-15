using Mapster;
using Microsoft.AspNetCore.CookiePolicy;
using Scalar.AspNetCore;
using Serilog;
using VC.Host;
using VC.Auth.Di;
using VC.Core.Di;
using VC.Host.Common;
using VC.Orders.Di;
using VC.Shared.Utilities;
using VC.Shared.Integrations.Di;
using VC.Shared.Utilities.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(VC.Auth.Api.Entry).Assembly)
    .AddApplicationPart(typeof(VC.Core.Api.Entry).Assembly)
    .AddApplicationPart(typeof(VC.Orders.Api.Entry).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.ConfigureUtilities(builder.Configuration);

builder.Services.ConfigureIntegrationsModule(builder.Configuration);
builder.Services.ConfigureAuthModule(builder.Configuration);
builder.Services.ConfigureCoreModule(builder.Configuration);
builder.Services.ConfigureOrdersModule(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpLogging();
builder.Services.AddHealthChecks();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.ConfigureHost();
builder.Services.AddMapster();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
await EFCoreAutoMigrator.ApplyUnAplliedMigrationsAsync(app);

app.UseCors("AllowAll");
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

app.UseHttpLogging();
app.MapControllers();

app.Run();