using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.NewsService
{
    public interface INewsService
    {
        Task<List<NewsDto>> GetNewsAsync();
        Task<NewsDto?> GetByIdAsync(Guid id);
        Task AggregateNews(CancellationToken cancellationToken);
    }
}

