using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Entities
{
    public class News
    { 
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } =string.Empty;
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateDate { get; set; }
        public string Text { get; set; } = string.Empty;

        public long AuthorId { get; set; }
        public Author Author { get; set; }

        public long SourceId { get; set; }
        public Source Source { get; set; }
        public long CategoryId {  get; set; }
        public Category Category { get; set; }

        public List<Comments> Comments { get; set; }
    }
}
