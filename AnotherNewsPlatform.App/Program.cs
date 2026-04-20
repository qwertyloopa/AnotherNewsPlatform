using AnotherNewsPlatform.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AnotherNewsPlatform.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddDbContext<AnpDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionStrings:Default")));
        builder.Services.AddControllersWithViews();
        builder.RegisterNewsService();
        builder.RegisterSourceService();
        var app = builder.Build();

        app.MapDefaultEndpoints();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        //app.UseAuthorization();
        //app.UseAuthentication();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}

