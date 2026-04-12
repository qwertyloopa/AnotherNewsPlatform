using System;
using AnotherNewsPlatform.Core.DTOs;
namespace AnotherNewsPlatform.App.Models;

public class ArticleModel
{
    
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public string Text { get; set; } 
        public DateTime PublishDate { get; set; }

        public long SourceId { get; set; }
        public string SourceName { get; set; } 

        public List<string> Comments { get; set; }
}
