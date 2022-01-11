using System;
using System.Collections.Generic;

namespace Online_Store.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubSection SubSection { get; set; }
        public Guid? SubSectionId { get; set; }
        public double Cost { get; set; }
        public string Information { get; set; }
        public string PictureGeneral { get; set; }
        public double Score { get; set; }
        public int ViewCount { get; set; }
        public int OrderCount { get; set; }
        public string Picture0 { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
        public List<Review> Reviews { get; set; }=new List<Review>();
    }
}
