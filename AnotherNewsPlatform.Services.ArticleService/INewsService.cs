using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Services.NewsService
{
    public interface INewsService
    {
        Task<List<ArticleDto>> GetNewsAsync();
        Task<ArticleDto?> GetByIdAsync(Guid id);
        Task AggregateNews(CancellationToken cancellationToken);
    }
}

