using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace AnotherNewsPlatform.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AnpDbContext>
    {
        public AnpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnpDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=AnotherNewsPlatformDb;Username=postgres;Password=rootroot");
            return new AnpDbContext(optionsBuilder.Options);
        }
    }
}
