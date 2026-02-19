using AnotherNewsPlatform.DataAccess.Configuration;
using AnotherNewsPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AnotherNewsPlatform.DataAccess
{
    internal class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Author> authors {  get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<NewsPublisher> newsPublishers { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=MASHINA3000;Database=AnotherNewsPortalDb;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsPublisherConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
