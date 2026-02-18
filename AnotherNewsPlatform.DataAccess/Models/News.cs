using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public DateTime PublicationDate = DateTime.UtcNow;
        public string Text { get; set; }
        public long AuthorId { get; set; }
    }
}
