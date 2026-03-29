using AnotherNewsPlatform.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Services.NewsService
{
    public interface INewsService
    {
        public Task<NewsDto[]> GetNewsAsync();
        public Task<NewsDto?> GetByIdAsync(Guid id);
        //public Task<List<NewsDto>> SearchAsync(string query);
    }
}
