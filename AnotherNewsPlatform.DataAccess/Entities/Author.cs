using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Entities
{
    public class Author
    { 
        public long Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public NewsPublisher newsPublisher { get; set; }
        public ICollection<News> News { get; set; }
    }
}
