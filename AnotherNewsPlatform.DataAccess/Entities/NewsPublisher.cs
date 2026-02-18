using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Entities
{
    public class NewsPublisher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Adress {  get; set; }
        public string EmailPrefix { get; set; }

        public ICollection<News> News { get; set; }

    }
}
