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
        public string Text { get; set; } = string.Empty;

        public long AuthorId { get; set; }
        public Author author { get; set; }

        public int PublisherId { get; set; }
        public NewsPublisher publisher { get; set; }
    }
}
