using System;
using System.Collections.Generic;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class IndexBasket
    {   public Guid basketId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<BasketProduct> ListProducts { get; set; } = new List<BasketProduct>();
    }
}
