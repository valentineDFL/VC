using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
VC.Tenants.Di.ModuleConfiguration.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
app.MapHealthChecks("/health");
VC.Tenants.Di.ModuleConfiguration.MapEndpoints(app);

app.Run();
