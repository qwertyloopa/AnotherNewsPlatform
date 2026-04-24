using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.Database.Entities
{
    [Index(nameof(OriginalUrl), Name = "Index_OriginalUrl", IsUnique = true)]
    public class Article
    { 
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } =string.Empty;
        public string Text { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        
        public long SourceId { get; set; }
        public Source Source { get; set; }
        //public long CategoryId {  get; set; }
        //public Category Category { get; set; }

        public List<Commentaries> Comments { get; set; }
    }
}

