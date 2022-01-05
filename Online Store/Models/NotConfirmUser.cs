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
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(2, ErrorMessage = "Минимальная длина поля-2 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(2, ErrorMessage = "Минимальная длина поля-2 символов")]
        public string Surname { get; set; }
        public DateTime DataBorn { get; set; }
        public int Year { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression(@"[+]{1}[3]{1}[7]{1}[5]{1}[0-9]{9}", ErrorMessage = "Некорректный номер")]
        public string Number { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
