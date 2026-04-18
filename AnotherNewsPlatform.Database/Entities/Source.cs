using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Database.Entities
{
    public class Source
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DomainUrl { get; set; }
        public string RssUrl { get; set; }
        public List<News> News { get; set; }

    }
}

