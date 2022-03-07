using System;
using Microsoft.EntityFrameworkCore;

namespace Online_Store.ViewModels
{
    [Keyless]
    public class CreateOrderViewModel
    {
        public Guid UserId { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressHome { get; set; }
        public string AddressApartment { get; set; }
        public decimal TotalCost { get; set; }
    }
}
