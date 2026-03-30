using AnotherNewsPlatform.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Services.NewsService
{
    public interface INewsService
    {
        public Task<News[]> GetNewsAsync();
        public Task
    }
}
