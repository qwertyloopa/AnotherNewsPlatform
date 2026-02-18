using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Models
{
    internal class NewsPublishers
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Adress {  get; set; }
        public string EmailPrefix { get; set; }
    }
}
