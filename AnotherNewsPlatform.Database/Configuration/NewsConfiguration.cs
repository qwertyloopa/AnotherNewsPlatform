using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnotherNewsPlatform.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Database.Configuration
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Title)
                .HasMaxLength(78)
                .IsRequired();
            builder.Property(n => n.Content).IsRequired();
            builder.Property(n => n.PublishDate).IsRequired();
            /*builder.HasOne(p => p.Author).WithMany(n => n.News)
                .HasForeignKey(k => k.AuthorId)
                .IsRequired(); */
            builder.HasOne(n => n.Source)
                .WithMany(p => p.News)
                .HasForeignKey(n => n.SourceId)
                .IsRequired();
            /*builder.HasOne(n => n.Category).WithMany(c => c.News)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();*/
        }
    }
}

