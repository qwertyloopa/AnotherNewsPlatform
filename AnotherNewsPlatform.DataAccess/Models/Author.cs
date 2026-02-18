using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Models
{
    public class Author
    { 
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<News> News { get; set; }

    }
}
