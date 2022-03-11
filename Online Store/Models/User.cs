using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Store.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public DateTime DataBorn { get; set; }
        public int Year { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public string Number { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Basket Basket { get; set; }
        public List<Review> Reviews { get; set; }=new List<Review>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public int ProductScore { get; set; }
        public uint Points { get; set; }
    }
}
