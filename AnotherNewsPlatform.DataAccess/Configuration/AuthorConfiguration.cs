using AnotherNewsPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Configuration
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Bio).HasMaxLength(500);
            builder.HasMany(a => a.News).WithOne(n => n.author)
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
