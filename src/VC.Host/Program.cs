using OpenTelemetry.Metrics;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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
builder.Services.AddHealthChecks();

VC.Tenants.Di.ModuleConfiguration.Configure(builder.Services, builder.Configuration);
VC.Bookings.Di.ModuleConfiguration.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();
app.MapHealthChecks("/health");
app.MapOpenApi();
app.MapScalarApiReference();
VC.Tenants.Di.ModuleConfiguration.MapEndpoints(app);
VC.Bookings.Di.ModuleConfiguration.MapEndpoints(app);

app.Run();
