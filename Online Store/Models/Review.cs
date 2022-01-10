using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Store.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public User AuthorUser { get; set; }
        public Product Product { get; set; }
        public Guid? ProductId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Picture0 { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
    }
}
