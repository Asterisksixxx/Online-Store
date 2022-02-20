using System;
using System.Collections.Generic;

namespace Online_Store.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Basket Basket { get; set; }
        public Guid BasketId { get; set; }
        public int ProductsCount { get; set; }
        public DateTime DateAndTime { get; set; }
        public double TotalCost { get; set; }
    }
}
