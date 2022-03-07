using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<BasketProduct> ListProducts { get; set; }=new List<BasketProduct>();
    }
}
