using System;
using System.Collections.Generic;

namespace Online_Store.Models
{
    public class Promocod
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<SubSection> SubSections { get; set; } = new List<SubSection>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiskontPercent { get; set; }
        public bool ActivateFlag { get; set; }
    }
}
