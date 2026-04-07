using AnotherNewsPlatform.DataAccess;
using AnotherNewsPlatform.NewsService;

namespace AnotherNewsPlatform.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddDbContext<AnpDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<INewsService, AnotherNewsPlatform.NewsService.NewsService>();
        var app = builder.Build();

        app.MapDefaultEndpoints();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
