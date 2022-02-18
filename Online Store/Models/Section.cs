using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models
{
    public class Section
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SubsectionCount { get; set; }
        public List<SubSection> SubSections { get; set; }=new List<SubSection>();

    }
}
