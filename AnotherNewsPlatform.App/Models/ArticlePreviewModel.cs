using System;
using AnotherNewsPlatform.Core.DTOs;
namespace AnotherNewsPlatform.App.Models;

public class ArticlePreviewModel
{
    
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public string OriginalUrl { get; set; }
        public string Text { get; set; } 
        public DateTime PublishDate { get; set; }

}

