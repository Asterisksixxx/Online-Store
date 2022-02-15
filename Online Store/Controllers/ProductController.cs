using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Store.Models;
using Online_Store.Services;

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
        public async Task<IActionResult> Create(Product product)
        {
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
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _productService.GetOneAsync(id));
        }
    }
}
