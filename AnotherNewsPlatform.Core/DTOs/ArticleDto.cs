using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Core.DTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public string OriginalUrl { get; set; } = string.Empty;
        public long SourceId { get; set; }

        //public List<string> Comments { get; set; } = new();
    }
}

