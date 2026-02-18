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
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=AnotherNewsPlatformDb;Trusted_Connection=True;TrustedServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
