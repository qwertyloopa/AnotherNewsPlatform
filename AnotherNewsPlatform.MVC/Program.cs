using Microsoft.EntityFrameworkCore;
using AnotherNewsPlatform.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AnpDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.RegisterNewsService();
builder.RegisterSourceService();
builder.RegisterUserService();
builder.Services.AddScoped<AnotherNewsPlatform.MVC.Mappers.Articles.DtoToArticlePreviewMapper>();
builder.Services.AddScoped<AnotherNewsPlatform.MVC.Mappers.Articles.CreateArticleModelToDtoMapper>();
// Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
// Log.Information("Первый пошёл");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
