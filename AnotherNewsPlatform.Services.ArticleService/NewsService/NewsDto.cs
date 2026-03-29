using AnotherNewsPlatform.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Services.NewsService
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Text { get; set; }

        public long AuthorId { get; set; }
        public Author Author { get; set; }

        public long SourceId { get; set; }
        public Source Source { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<string> Comments { get; set; }
    }
}
