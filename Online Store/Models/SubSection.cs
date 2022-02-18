using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models
{
    public class SubSection
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Section Section { get; set; }
        [Required]
        public Guid? SectionId { get; set; }
        public List<Product> Product { get; set; } =new List<Product>();
        public uint ProductCount { get; set; }
    }
}
