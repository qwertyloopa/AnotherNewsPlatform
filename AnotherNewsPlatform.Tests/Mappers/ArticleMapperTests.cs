using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.Database.Entities;
using Xunit;

namespace AnotherNewsPlatform.Tests.Mappers
{
    public class ArticleMapperTests
    {
        private readonly ArticleMapper _mapper;

        public ArticleMapperTests()
        {
            _mapper = new ArticleMapper();
        }

        [Fact]
        public void ToDto_WhenEntityHasAllProperties_ReturnsCorrectDto()
        {
            // Arrange
            var entity = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Test Article Title",
                Content = "Test Article Content",
                Text = "Test Article Text",
                OriginalUrl = "https://example.com/article",
                PublishDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                SourceId = 123,
                Source = new Source { Id = 123, Name = "Test Source" },
            };

            // Act
            var result = _mapper.ToDto(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Title, result.Title);
            Assert.Equal(entity.Content, result.Content);
            Assert.Equal(entity.Text, result.Text);
            Assert.Equal(entity.OriginalUrl, result.OriginalUrl);
            Assert.Equal(entity.PublishDate, result.PublishDate);
            Assert.Equal(entity.SourceId, result.SourceId);
        }

        [Fact]
        public void ToDto_WhenEntityHasNullProperties_ReturnsDtoWithDefaults()
        {
            // Arrange
            var entity = new Article
            {
                Id = Guid.NewGuid(),
                Title = null!,
                Content = null!,
                Text = null!,
                OriginalUrl = null!,
                PublishDate = DateTime.UtcNow,
                SourceId = 0,
                Source = null!,
            };

            // Act
            var result = _mapper.ToDto(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            // Mapperly likely preserves null values
            Assert.Null(result.Title);
            Assert.Null(result.Content);
            Assert.Null(result.Text);
            Assert.Null(result.OriginalUrl);
            Assert.Equal(entity.PublishDate, result.PublishDate);
            Assert.Equal(0, result.SourceId);
        }

        [Fact]
        public void ToEntity_WhenDtoHasAllProperties_ReturnsCorrectEntity()
        {
            // Arrange
            var dto = new ArticleDto
            {
                Id = Guid.NewGuid(),
                Title = "Test DTO Title",
                Content = "Test DTO Content",
                Text = "Test DTO Text",
                OriginalUrl = "https://example.com/dto-article",
                PublishDate = new DateTime(2024, 2, 20, 14, 45, 0, DateTimeKind.Utc),
                SourceId = 456
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Id, result.Id);
            Assert.Equal(dto.Title, result.Title);
            Assert.Equal(dto.Content, result.Content);
            Assert.Equal(dto.Text, result.Text);
            Assert.Equal(dto.OriginalUrl, result.OriginalUrl);
            Assert.Equal(dto.PublishDate, result.PublishDate);
            Assert.Equal(dto.SourceId, result.SourceId);
            Assert.Null(result.Source);
        }

        [Fact]
        public void ToEntity_WhenDtoHasNullProperties_ReturnsEntityWithDefaults()
        {
            // Arrange
            var dto = new ArticleDto
            {
                Id = Guid.NewGuid(),
                Title = null!,
                Content = null!,
                Text = null!,
                OriginalUrl = null!,
                PublishDate = DateTime.UtcNow,
                SourceId = 0
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Id, result.Id);
            // Mapperly likely preserves null values
            Assert.Null(result.Title);
            Assert.Null(result.Content);
            Assert.Null(result.Text);
            Assert.Null(result.OriginalUrl);
            Assert.Equal(dto.PublishDate, result.PublishDate);
            Assert.Equal(0, result.SourceId);
            Assert.Null(result.Source);
        }

        [Fact]
        public void ToDto_IgnoresSourceAndCommentsProperties()
        {
            // Arrange
            var entity = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Test Article",
                Content = "Content",
                Text = "Text",
                OriginalUrl = "https://example.com",
                PublishDate = DateTime.UtcNow,
                SourceId = 789,
                Source = new Source { Id = 789, Name = "Should Be Ignored" },
            };

            // Act
            var result = _mapper.ToDto(entity);

            // Assert
            Assert.NotNull(result);
            // Source property should not be mapped to DTO (DTO doesn't have Source property)
            // Comments property should not be mapped to DTO (DTO doesn't have Comments property)
            // The test verifies that mapping succeeds despite these properties being present in entity
            Assert.Equal(entity.SourceId, result.SourceId);
        }

        [Fact]
        public void ToEntity_IgnoresSourceAndCommentsProperties()
        {
            // Arrange
            var dto = new ArticleDto
            {
                Id = Guid.NewGuid(),
                Title = "Test DTO",
                Content = "Content",
                Text = "Text",
                OriginalUrl = "https://example.com",
                PublishDate = DateTime.UtcNow,
                SourceId = 999
            };

            // Act
            var result = _mapper.ToEntity(dto);

            // Assert
            Assert.NotNull(result);
            // Source and Comments should remain null in entity (they are ignored)
            Assert.Null(result.Source);
            // Other properties should be mapped correctly
            Assert.Equal(dto.SourceId, result.SourceId);
        }

        [Fact]
        public void ToDto_ThenToEntity_ReturnsEquivalentObject()
        {
            // Arrange
            var originalEntity = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Round Trip Test",
                Content = "Content for round trip",
                Text = "Text for round trip",
                OriginalUrl = "https://example.com/roundtrip",
                PublishDate = DateTime.UtcNow.AddDays(-5),
                SourceId = 111,
                Source = new Source { Id = 111, Name = "Test Source" },
            };

            // Act
            var dto = _mapper.ToDto(originalEntity);
            var resultEntity = _mapper.ToEntity(dto);

            // Assert
            Assert.NotNull(dto);
            Assert.NotNull(resultEntity);
            
            // Compare properties that should be preserved
            Assert.Equal(originalEntity.Id, resultEntity.Id);
            Assert.Equal(originalEntity.Title, resultEntity.Title);
            Assert.Equal(originalEntity.Content, resultEntity.Content);
            Assert.Equal(originalEntity.Text, resultEntity.Text);
            Assert.Equal(originalEntity.OriginalUrl, resultEntity.OriginalUrl);
            Assert.Equal(originalEntity.PublishDate, resultEntity.PublishDate);
            Assert.Equal(originalEntity.SourceId, resultEntity.SourceId);
            
            // Source and Comments should be null (ignored during mapping)
            Assert.Null(resultEntity.Source);
        }
    }
}