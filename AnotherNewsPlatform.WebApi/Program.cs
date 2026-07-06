using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.WebApi.Infrastructure;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        retainedFileCountLimit: 7)
    .WriteTo.File(
        path: "logs/log-.json",
        rollingInterval: RollingInterval.Day,
        formatter: new Serilog.Formatting.Json.JsonFormatter(renderMessage: true),
        retainedFileCountLimit: 7));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    
});
builder.Services.AddDbContext<AnpDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.RegisterNewsService();
builder.RegisterSourceService();
builder.RegisterUserService();
builder.RegisterTokenService();
builder.Services.AddScoped<AnotherNewsPlatform.WebApi.Mappers.ArticleMapper>();
builder.Services.AddScoped<AnotherNewsPlatform.WebApi.Mappers.UserMapper>();
builder.RegisterCoreMappers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(InsertArticleDataCommand).Assembly);
});
builder.Services.AddScoped<FluentValidatorActionFilter>();
builder.AddJwtAuthentication();
builder.SetupHangfire();
builder.Services.AddScoped<HangfireJobs>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard();

// Регистрация рекуррентных задач Hangfire при старте приложения
app.Services.GetRequiredService<IRecurringJobManager>().AddOrUpdate<HangfireJobs>(
    "AggregateNewsJob",
    job => job.AggregateNewsJob(CancellationToken.None),
    Cron.MinuteInterval(15));

app.Services.GetRequiredService<IRecurringJobManager>().AddOrUpdate<HangfireJobs>(
    "RateUnratedNewsJob",
    job => job.RateUnratedNewsJob(CancellationToken.None),
    Cron.Hourly());

app.Run();
