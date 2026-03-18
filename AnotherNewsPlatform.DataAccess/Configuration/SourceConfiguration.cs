using AnotherNewsPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Configuration
{
    public class SourceConfiguration : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.ToTable("Sources");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.DomainUrl).HasMaxLength(500);
            builder.Property(p => p.RssUrl).HasMaxLength(550);
            builder.HasMany(p => p.News).WithOne(n => n.Source)
                .HasForeignKey(n => n.SourceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
