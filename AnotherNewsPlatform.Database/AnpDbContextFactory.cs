using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Database
{
    public class AnpDbContextFactory : IDesignTimeDbContextFactory<AnpDbContext> //для миграций
    {
        public AnpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnpDbContext>();
            optionsBuilder.UseNpgsql("Server = 127.0.0.1; Port = 5432; Database = AnotherNewsPlatformDb; User Id = postgres; Password = rootroot;");
            return new AnpDbContext(optionsBuilder.Options);
        }
    }
}

