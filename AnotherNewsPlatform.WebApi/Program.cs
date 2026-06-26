using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnpDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.RegisterNewsService();
builder.RegisterSourceService();
builder.RegisterUserService();
builder.RegisterTokenService();
//builder.Services.AddScoped<AnotherNewsPlatform.MVC.Mappers.ArticleMapper>();
//builder.Services.AddScoped<AnotherNewsPlatform.MVC.Mappers.UserMapper>();
builder.RegisterCoreMappers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(InsertArticleDataCommand).Assembly);
});
builder.Services.AddScoped<FluentValidatorActionFilter>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add CORS policy for Blazor client
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient",
        policy =>
        {
            policy.WithOrigins(
                "https://localhost:7161",  // Blazor HTTPS
                "http://localhost:5256",   // Blazor HTTP
                "https://localhost:7238",  // WebApi itself (for testing)
                "http://localhost:5027"    // WebApi HTTP
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});*/

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy - must be before UseAuthorization and MapControllers
app.UseCors("BlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
