using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Store.Models;
using Online_Store.Services;
using Online_Store.ViewModels;

namespace Online_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubSectionService _subSectionService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IProductService productService, ISubSectionService subSectionService, IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _subSectionService = subSectionService;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var subSec = await _subSectionService.GetAllAsyncForProduct();
            ViewBag.Subsection = new SelectList(subSec,"Id","Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel cpvm)
        {
            cpvm.Cost= cpvm.Cost.Replace(".", ",");
            var cost = Convert.ToDecimal(cpvm.Cost);
            Product product = new Product
            {
                Name = cpvm.Name,
                Information = cpvm.Information,
                Cost = cost,
                Count = cpvm.Count,
                PictureGeneralFile = cpvm.PictureGeneralFile,
                PictureSecondFile = cpvm.PictureSecondFile,
                PictureSubSecondFile = cpvm.PictureSubSecondFile,
                SubSectionId = cpvm.SubSectionId,
                SubSection = cpvm.SubSection
            };
            await _productService.SavePicture(product);
            await _productService.CreateAsync(product);
           return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var subSec = await _subSectionService.GetAllAsyncForProduct();
            ViewBag.Subsection = new SelectList(subSec, "Id", "Name");
            return View(await _productService.GetOneAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _productService.Update(product);
          return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await _productService.GetOneAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid productId)
        {
            return View(await _productService.GetOneAsync(productId));
        }
    }
}
