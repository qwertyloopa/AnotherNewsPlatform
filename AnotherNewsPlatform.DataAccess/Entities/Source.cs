using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Entities
{
    public class Source
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Adress {  get; set; }
        public string Url { get; set; }

        public List<News> News { get; set; }

    }
}
