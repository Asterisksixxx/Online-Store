using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Store.Models
{
    public class NotConfirmUser
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [MinLength(2, ErrorMessage = "Min length-2 symbol")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [MinLength(2, ErrorMessage = "Min length-2 symbol")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public DateTime DataBorn { get; set; }
        public int Year { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [RegularExpression(@"[+]{1}[3]{1}[7]{1}[5]{1}[0-9]{9}", ErrorMessage = "Not correct phone number")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
