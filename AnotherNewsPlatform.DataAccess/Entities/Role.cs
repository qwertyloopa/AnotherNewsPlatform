using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnotherNewsPlatform.DataAccess.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
