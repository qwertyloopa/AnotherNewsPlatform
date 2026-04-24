//using AnotherNewsPlatform.Database.Configuration;
using AnotherNewsPlatform.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AnotherNewsPlatform.Database
{
    public class AnpDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Commentaries> Commentaries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<User> Users { get; set; }

        
        public AnpDbContext(DbContextOptions<AnpDbContext> options) : base(options) { }
    }
}

