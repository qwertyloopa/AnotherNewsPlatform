using AnotherNewsPlatform.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AnotherNewsPlatform.DataAccess
{
    internal class AnpDbContext : DbContext
    {
        public DbSet<Author> authors {  get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<NewsPublishers> newsPublishers { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=AnotherNewsPlatformDb;Trusted_Connection=True;TrustedServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
