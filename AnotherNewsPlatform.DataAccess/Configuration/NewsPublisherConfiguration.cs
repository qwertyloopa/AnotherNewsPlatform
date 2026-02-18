using AnotherNewsPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Configuration
{
    public class NewsPublisherConfiguration : IEntityTypeConfiguration<NewsPublisher>
    {
        public void Configure(EntityTypeBuilder<NewsPublisher> builder)
        {
            builder.ToTable("NewsPublishers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.Url).HasMaxLength(500);
            builder.HasMany(p => p.News).WithOne(n => n.publisher)
                .HasForeignKey(n => n.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
