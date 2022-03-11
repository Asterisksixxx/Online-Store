using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class UpdateProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubSection SubSection { get; set; }
        public Guid? SubSectionId { get; set; }
        [DataType(DataType.Currency)]
        public string Cost { get; set; }
        [Range(0, 99999, ErrorMessage = "Ошибка в количестве товара")]
        public int Count { get; set; }
        public string Information { get; set; }
        public double Score { get; set; }
        public int ViewCount { get; set; }
        public int OrderCount { get; set; }
    }
}
