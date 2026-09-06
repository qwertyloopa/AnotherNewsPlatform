using System.Diagnostics;
using AnotherNewsPlatform.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using OpenTelemetry.Trace;

using AnotherNewsPlatform.Database.Entities;

namespace AnotherNewsPlatform.MigrationService;


public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity(
            "Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AnpDbContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(AnpDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(AnpDbContext dbContext, CancellationToken cancellationToken)
    {
        var roles = new List<Role>();
        roles.AddRange(
            new Role()
                {
                    Id = 1,
                    Name = "Admin",
                    Users = null,
                }, 
                new Role()
                {
                    Id = 2,
                    Name = "User",
                    Users = null,
                }
        );
        var sources = new List<Source>();
        sources.AddRange(
                new Source()
                {
                    Id= 1,
                    Name= "NEWS.BY | Белтелерадиокомпания",
                    DomainUrl = "https://news.by",
                    RssUrl = "https://news.by/feed/rss-google.xml",
                    Articles = null,
                },
                new Source()
                {
                    Id = 2,
                    Name = "Onliner",
                    DomainUrl = "https://onliner.by/",
                    RssUrl = "https://onliner.by/feed",
                    Articles = null,
                }
            );
        

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database
                .BeginTransactionAsync(cancellationToken);

            await dbContext.Sources.AddRangeAsync(sources, cancellationToken);
            await dbContext.Roles.AddRangeAsync(roles, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
