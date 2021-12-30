using System;
using System.Collections.Generic;

namespace Online_Store.Models
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubSection> SubSections=new List<SubSection>();

    }
}
