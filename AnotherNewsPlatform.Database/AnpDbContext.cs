//using AnotherNewsPlatform.Database.Configuration;
using AnotherNewsPlatform.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AnotherNewsPlatform.Database
{
    public class AnpDbContext(DbContextOptions<AnpDbContext> options) : DbContext(options)
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Commentary> Commentaries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnpDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

