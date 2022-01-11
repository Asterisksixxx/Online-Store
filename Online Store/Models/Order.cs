using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Store.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<Product> Products { get; set; }=new List<Product>();
        public int ProductsCount { get; set; }
        public DateTime DateAndTime { get; set; }
        public double TotalCost { get; set; }
    }
}
