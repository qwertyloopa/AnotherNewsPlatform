using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.CQS;
using AnotherNewsPlatform.Database.Entities;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using MediatR;
using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.CQS.Articles.QueryHandlers;
using AnotherNewsPlatform.Services.NewsService.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AnotherNewsPlatform.Services.NewsService
{
    public class NewsService(
        IMediator mediator,
        AnpDbContext dbContext,
        IRssReader rssReader,
        IWebScraper webScraper,
        IConfiguration configuration
        ) : INewsService
    {
        

        public async Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var result = await mediator.Send(new GetArticleById(id), token);
            return result;
        }

        public async Task CreateNews(ArticleDto article, CancellationToken cancellationToken)
        {
            await mediator.Send(new InsertArticleCommand { Article = article }, cancellationToken);
        }

        public async Task UpdatePartialArticleAsync(Guid id, string? updatedArticleTitle, decimal? updatedArticleRate, CancellationToken cancellationToken)
        {
            var command = new UpdatePartialArticleCommand(id: id, updatedArticleRate: updatedArticleRate,  updatedArticleTitle: updatedArticleTitle);
            await mediator.Send(command, cancellationToken);
        }

        public async Task<IReadOnlyCollection<ArticleDto>> GetNewsByRateAndSource(decimal? minRate, int? sourceId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetArticleByRateAndSourceQuery(minRate, sourceId), cancellationToken);
        }

        public async Task<IReadOnlyCollection<ArticleDto>> GetNewsByPage(int pageNumber, int pageSize)
        {
            return await mediator.Send(new GetArticleByPageQuery(pageNumber, pageSize));
        }

        public async Task<int> GetNewsCountAsync(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetArticleCountQuery(), cancellationToken);
        }
    }
}
