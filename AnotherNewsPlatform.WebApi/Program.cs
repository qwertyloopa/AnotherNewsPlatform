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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
