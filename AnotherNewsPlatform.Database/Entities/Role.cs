using System;
using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations; // moved configuration to Fluent API
// using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnotherNewsPlatform.Database.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}

