using AnotherNewsPlatform.DataAccess.Configuration;
using AnotherNewsPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AnotherNewsPlatform.DataAccess
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Author> Authors {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Source> Source { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=127.0.0.1;Port=5432;Database=AnotherNewsPlatformDb;User Id=postgres;Password=rootroot;";
            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new SourceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
