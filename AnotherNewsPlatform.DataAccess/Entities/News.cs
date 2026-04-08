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
        
        public long SourceId { get; set; }
        public Source Source { get; set; }
        //public long CategoryId {  get; set; }
        //public Category Category { get; set; }

        public List<Comments> Comments { get; set; }
    }
}
