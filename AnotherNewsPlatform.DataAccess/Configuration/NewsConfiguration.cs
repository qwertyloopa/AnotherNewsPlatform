using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnotherNewsPlatform.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Configuration
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
            builder.HasOne(p => p.author).WithMany(n => n.News)
                .HasForeignKey(k => k.AuthorId)
                .IsRequired();
            builder.HasOne(n => n.publisher)
                .WithMany(p => p.News)
                .HasForeignKey(n => n.PublisherId)
                .IsRequired();

        }
    }
}
