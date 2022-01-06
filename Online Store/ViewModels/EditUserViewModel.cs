using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DataBorn { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
    }
}
