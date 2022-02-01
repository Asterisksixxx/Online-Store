using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        [Required]
       public string Name { get; set; }
    }
}
