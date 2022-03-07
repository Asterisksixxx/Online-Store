using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Address { get; set; }
        public DateTime Date {get; set; }
        public decimal TotalCost { get; set; }
    }
}
