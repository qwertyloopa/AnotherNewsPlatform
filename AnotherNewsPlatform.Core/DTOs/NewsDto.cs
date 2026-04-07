using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.NewsService
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }

        public long AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public long SourceId { get; set; }
        public string SourceName { get; set; } = string.Empty;

        public long CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public List<string> Comments { get; set; } = new();
    }
}
