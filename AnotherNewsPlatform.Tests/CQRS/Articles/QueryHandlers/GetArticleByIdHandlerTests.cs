using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.CQS.Articles.QueryHandlers;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AnotherNewsPlatform.Tests.CQRS.Articles.QueryHandlers;

public class GetArticleByIdHandlerTests
{
    private readonly Mock<AnpDbContext> _dbContextMock;
    private readonly GetArticleByIdHandler _handler;

    public GetArticleByIdHandlerTests()
    {
        _dbContextMock = new Mock<AnpDbContext>();
        _handler = new GetArticleByIdHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_WhenArticleExists_ReturnsArticleDto()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var articleEntity = new Article
        {
            Id = articleId,
            Title = "Test Article",
            Content = "Test Content",
            Text = "Test Text",
            OriginalUrl = "https://example.com",
            PublishDate = DateTime.UtcNow,
            SourceId = 1
        };

        var data = new List<Article> { articleEntity }.AsQueryable();
        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        mockSet.As<IAsyncEnumerable<Article>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<Article>(data.GetEnumerator()));
        mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Article, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((System.Linq.Expressions.Expression<Func<Article, bool>> predicate, CancellationToken token) =>
                data.FirstOrDefault(predicate.Compile()));

        _dbContextMock.Setup(db => db.Articles).Returns(mockSet.Object);

        var request = new GetArticleById(articleId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(articleId, result.Id);
        Assert.Equal(articleEntity.Title, result.Title);
        Assert.Equal(articleEntity.Content, result.Content);
        Assert.Equal(articleEntity.Text, result.Text);
        Assert.Equal(articleEntity.OriginalUrl, result.OriginalUrl);
        Assert.Equal(articleEntity.PublishDate, result.PublishDate);
        Assert.Equal(articleEntity.SourceId, result.SourceId);
    }

    [Fact]
    public async Task Handle_WhenArticleDoesNotExist_ReturnsNull()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var data = new List<Article>().AsQueryable();
        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        mockSet.As<IAsyncEnumerable<Article>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<Article>(data.GetEnumerator()));
        mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Article, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Article?)null);

        _dbContextMock.Setup(db => db.Articles).Returns(mockSet.Object);

        var request = new GetArticleById(articleId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenArticleHasNullProperties_ReturnsDtoWithNulls()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var articleEntity = new Article
        {
            Id = articleId,
            Title = null!,
            Content = null!,
            Text = null!,
            OriginalUrl = null!,
            PublishDate = DateTime.UtcNow,
            SourceId = 0
        };

        var data = new List<Article> { articleEntity }.AsQueryable();
        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        mockSet.As<IAsyncEnumerable<Article>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<Article>(data.GetEnumerator()));
        mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Article, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(articleEntity);

        _dbContextMock.Setup(db => db.Articles).Returns(mockSet.Object);

        var request = new GetArticleById(articleId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(articleId, result.Id);
        Assert.Null(result.Title);
        Assert.Null(result.Content);
        Assert.Null(result.Text);
        Assert.Null(result.OriginalUrl);
        Assert.Equal(articleEntity.PublishDate, result.PublishDate);
        Assert.Equal(0, result.SourceId);
    }

    [Fact]
    public async Task Handle_WhenCancellationTokenIsCancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        var request = new GetArticleById(articleId);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _handler.Handle(request, cancellationTokenSource.Token));
    }

    // Вспомогательный класс для поддержки асинхронного перечисления
    private class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current => _inner.Current;

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }
    }
}