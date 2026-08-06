using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<AnpDbContext>("AnotherNewsPlatformDb");

var host = builder.Build();
host.Run();