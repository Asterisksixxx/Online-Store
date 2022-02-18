using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.ViewModels;

namespace Online_Store.Services
{
   public interface IHomeService
    {
        Task<IndexViewModel> GetAllData(Guid id);
    }

    public class HomeService : IHomeService
    {
        private readonly AppDataContext _appDataContext;
        private readonly ISectionService _sectionService;
        private readonly ISubSectionService _subSectionService;
        private readonly IProductService _productService;

        public HomeService(AppDataContext appDataContext, ISubSectionService subSectionService, ISectionService sectionService, IProductService productService)
        {
            _appDataContext = appDataContext;
            _subSectionService = subSectionService;
            _sectionService = sectionService;
            _productService = productService;
        }

        public async Task<IndexViewModel> GetAllData(Guid id)
        {
            var subSection = await _subSectionService.GetOneAsync(id);
            if (subSection!=null)
            {
                var sections= (await _sectionService.GetAllAsync()).ToList();
                var products = (await _productService
                    .GetAllFromSubSectionsAsync(subSection.Id)).ToList();
                return new IndexViewModel
                {
                    Product = products,
                    Sections = sections
                };
            }
            else
            {
                var products = (await _productService.GetAllAsync()).ToList();
                var sections = (await _sectionService.GetAllAsync()).ToList();
                return new IndexViewModel
                {
                    Product = products,
                    Sections = sections
                };
            }
        
        }
    }
}
