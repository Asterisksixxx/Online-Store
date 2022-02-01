using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Store.Data;
using Online_Store.ViewModels;

namespace Online_Store.Services
{
   public interface IHomeService
    {
        Task<IndexViewModel> GetAllData();
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

        public async Task<IndexViewModel> GetAllData()
        {
            var products = (await _productService.GetAllAsync()).ToList();
            var subsections = (await _subSectionService.GetAllAsync()).ToList();
            var sections = (await _sectionService.GetAllAsync()).ToList();
            return new IndexViewModel
            {
                Product =products,
                Section = sections,
                SubSections = subsections
            };
        }
    }
}
