using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Online_Store.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubSection SubSection { get; set; }
        public Guid? SubSectionId { get; set; }
        public double Cost { get; set; }
        [Range(0,99999,ErrorMessage = "Ошибка в количестве товара")]
        public int Count { get; set; }
        public string Information { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string PictureGeneral { get; set; }
        [NotMapped]
        [DisplayName("Upload picture")]
        public IFormFile PictureGeneralFile { get; set; }
        public double Score { get; set; }
        public int ViewCount { get; set; }
        public int OrderCount { get; set; }
        public List<Review> Reviews { get; set; }=new List<Review>();
    }
}
