using AnotherNewsPlatform.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnotherNewsPlatform.Database.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.OriginalUrl)
                .HasDatabaseName("Index_OriginalUrl")
                .IsUnique();

            builder.HasOne(a => a.Source)
                .WithMany(s => s.Articles)
                .HasForeignKey(a => a.SourceId);
        }
    }
}
