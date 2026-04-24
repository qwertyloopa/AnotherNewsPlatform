using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnotherNewsPlatform.Database.Entities
{
    public class Source
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string DomainUrl { get; set; }
        public string RssUrl { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}

