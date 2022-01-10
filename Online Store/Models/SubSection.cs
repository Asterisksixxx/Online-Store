using System;
using System.Collections.Generic;

namespace Online_Store.Models
{
    public class SubSection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Section Section { get; set; }
        public Guid? SectionId { get; set; }
        public List<Product> Product { get; set; } =new List<Product>();
        public uint ProductCount { get; set; }
    }
}
