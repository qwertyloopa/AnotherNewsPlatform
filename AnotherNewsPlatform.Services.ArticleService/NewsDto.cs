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
    }
}
