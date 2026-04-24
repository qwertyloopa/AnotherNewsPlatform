using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnotherNewsPlatform.Database.Entities
{
    public class Commentaries
    {
        [Key]
        public Guid Id {  get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public Guid NewsId { get; set; }
        public Article Article { get; set; }


        public string Text { get; set; }
    }
}

