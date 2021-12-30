using System;


namespace Online_Store.Models
{
    public class SubSection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Section Section { get; set; }
        public Guid SectionId { get; set; }
    }
}
