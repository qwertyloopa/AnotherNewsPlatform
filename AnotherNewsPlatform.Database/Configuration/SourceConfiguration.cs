using AnotherNewsPlatform.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.Database.Configuration
{
    public class SourceConfiguration : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Articles)
                .WithOne(a => a.Source)
                .HasForeignKey(a => a.SourceId);
        }
    }
}
