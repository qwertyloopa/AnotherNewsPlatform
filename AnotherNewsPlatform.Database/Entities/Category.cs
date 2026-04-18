using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Database.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<News> News { get; set; }
    }
}

