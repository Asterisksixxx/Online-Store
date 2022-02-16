using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Online_Store.Models;

namespace Online_Store.ViewModels
{
    public class CreateProductViewModel
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
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string PictureGeneral { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name1")]
        public string PictureSecond { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name2")]
        public string PictureSubSecond { get; set; }
        [NotMapped]
        [DisplayName("Главное изображение")]
        public IFormFile PictureGeneralFile { get; set; }
        [NotMapped]
        [DisplayName("Изображение")]
        public IFormFile PictureSecondFile { get; set; }
        [NotMapped]
        [DisplayName("Изображение1")]
        public IFormFile PictureSubSecondFile { get; set; }
    }
}
