using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnotherNewsPlatform.Core.DTOs
{
    public class RssNewsInfoDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string OriginalUrl { get; set; }
        //public required string Text { get; set; }
        public required long SourceId { get; set; }
        public DateTime PublishDate { get; set; }

        
    }
}
