using System;

namespace Online_Store.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubSection SubSection { get; set; }
        public Guid SubSectionId { get; set; }
        public decimal Cost { get; set; }
        public string Information { get; set; }
        public string PictureLink { get; set; }
    }
}
