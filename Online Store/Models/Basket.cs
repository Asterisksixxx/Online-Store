using System;
using System.Collections.Generic;

namespace Online_Store.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<Product> ListProducts { get; set; }=new List<Product>();
        
    }
}
