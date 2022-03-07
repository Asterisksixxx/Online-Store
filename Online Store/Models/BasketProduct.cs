using System;

namespace Online_Store.Models
{
    public class BasketProduct
    {
        public Guid Id { get; set; }
        public Basket Basket { get; set; }
        public Guid BasketId { get; set; }
        public Product Product { get; set; }
        public uint Count { get; set; }
        public decimal SumCost { get; set; }
    }
}
