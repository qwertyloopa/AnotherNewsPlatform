using AnotherNewsPlatform.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.NewsService
{
    public interface INewsService
    {
        Task<List<NewsDto>> GetNewsAsync();
        Task<NewsDto?> GetByIdAsync(Guid id);
        Task AggregateNewsAsync(CancellationToken cancellationToken);
    }
}
